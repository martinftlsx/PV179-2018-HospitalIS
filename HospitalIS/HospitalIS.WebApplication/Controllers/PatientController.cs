using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.Facades;
using HospitalIS.WebApplication.Models;
using HospitalIS.WebApplication.Models.Disease;
using HospitalIS.WebApplication.Models.HealthCard;
using HospitalIS.WebApplication.Models.Patient;

namespace HospitalIS.WebApplication.Controllers
{
    public class PatientController : Controller
    {
        public PatientFacade PatientFacade { get; set; }

        public DiseaseFacade DiseaseFacade { get; set; }

        public DiseaseAndSympthomFacade DiseaseAndSympthomFacade { get; set; }

        public HealthCardFacade HealthCardFacade { get; set; }

        public SympthomFacade SympthomFacade { get; set; }

        public DiseaseToHealthCardFacade DiseaseToHealthCardFacade { get; set; }

        public DoctorFacade DoctorFacade { get; set; }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PatientCreateModel patientCreateModel)
        {

            if (!ModelState.IsValid) return View(patientCreateModel);
            PatientDto patientDto = new PatientDto
            {
                Username = patientCreateModel.Username,
                Password = patientCreateModel.Password,
                Name = patientCreateModel.Name,
                Surname = patientCreateModel.Surname,
                DateOfBirth = patientCreateModel.DateOfBirth,
                Email = patientCreateModel.Email,
                IdentificationNumber = patientCreateModel.IdentificationNumber,
                ProfileCreationDate = DateTime.Now
            };             
            
            if (await PatientFacade.GetPatientByUsernameAsync(patientCreateModel.Username) != null ||
            await DoctorFacade.GetDoctorByUsernameAsync(patientCreateModel.Username) != null)
            {
                ModelState.AddModelError("Username", "Account with that username already exists");
                return View(patientCreateModel);
            }
            var patientId = await PatientFacade.RegisterPatient(patientDto);
            return RedirectToAction("Index", "Home");                
        }

        public async Task<ActionResult> PatientAskForGetInfoDoctor(string identificationNumber)
        {
            var patient = (await PatientFacade.GetPatientByIdentificationNumberAsync(identificationNumber));
            return await GetPatientInfo(patient);
        }

        public async Task<ActionResult> PatientAskForGetInfoPatient()
        {
            string username = HttpContext.User.Identity.Name;
            var patient = await PatientFacade.GetPatientByUsernameAsync(username);
            return await GetPatientInfo(patient);
        }

        private async Task<ActionResult> GetPatientInfo(PatientDto patient)
        {
            PatientInfoModel model = new PatientInfoModel
            {
                Name = patient.Name,
                Surname = patient.Surname,
                DateOfBirth = patient.DateOfBirth,
                Email = patient.Email,
                IdentificationNumber = patient.IdentificationNumber,
                ProfileCreationDate = patient.ProfileCreationDate,
                Doctors = await PatientFacade.GetDoctorsForPatientAsync(patient.Id),
            };

            patient.HealthCard = await PatientFacade.GetPatientWithHealthCardAsync(patient.Id);
            if (patient.HealthCard != null)
            {
                model.Diseases = await HealthCardFacade.GetDiseasesForHealthCard(patient.Id);
                model.BloodType = patient.HealthCard.BloodType;
            }
            return View("PatientAskForGetInfo", model);
        }

        public async Task<ActionResult> Update()
        {
            var patientData = await PatientFacade.GetPatientByUsernameAsync(HttpContext.User.Identity.Name);
            PatientUpdateModel patientUpdateModel = new PatientUpdateModel
            {
                Name = patientData.Name,
                Surname = patientData.Surname,
                Email = patientData.Email
            };           
            return View(patientUpdateModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(PatientUpdateModel patientUpdateModel)
        {
            if (!ModelState.IsValid) return View(patientUpdateModel);
            var username = HttpContext.User.Identity.Name;
            var patientToUpdate = await PatientFacade.GetPatientByUsernameAsync(username);

            patientToUpdate.Email = patientUpdateModel.Email;
            patientToUpdate.Name = patientUpdateModel.Name;
            patientToUpdate.Surname = patientUpdateModel.Surname;
            await PatientFacade.EditPatientAsync(patientToUpdate);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddDiseaseHealthCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddDiseaseHealthCard(AddDiseaseModel addDiseaseModel)
        {
            List<Guid> existingSympthomGuids = new List<Guid>();
            DiseaseDto diseaseToCreateDto = new DiseaseDto { Name = addDiseaseModel.Name };
            var diseaseGuid = await DiseaseFacade.CreateDiseaseAsync(diseaseToCreateDto);
            var sympthomStrings = addDiseaseModel.Sypthoms.Split(',');
            foreach (var sympthom in sympthomStrings)
            {
                var sympthomsByName = await SympthomFacade.GetSympthomByNameAsync(sympthom);
                if (sympthomsByName.Count() != 0)
                {
                    existingSympthomGuids.Add(sympthomsByName.First().Id);
                }
                else
                {
                    SympthomDto newSympthomDto = new SympthomDto { Name = sympthom };
                    existingSympthomGuids.Add(await SympthomFacade.CreateSympthomAsync(newSympthomDto));
                }
            }
            await MatchDiseaseAndSypthoms(diseaseGuid, existingSympthomGuids);
            string identificationNum = await UpdateHealthCard(new HealthCardUpdateModel { DiseaseId = diseaseGuid });
            return RedirectToAction("PatientAskForGetInfoDoctor", "Patient", new { identificationNumber = identificationNum });
        }

        public async Task<ActionResult> AddExistingDiseaseHealthCard()
        {
            var queryResult = await DiseaseFacade.GetAllDiseasesAsync();
            var diseases = queryResult.Items;
            var diseasesModel = new AllDiseasesModel
            {
                Diseases = diseases.Select(disease => disease.Name)
            };
            return View(diseasesModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddExistingDiseaseHealthCard(AllDiseasesModel allDiseasesModel)
        {
            var disease = (await DiseaseFacade.GetDiseaseByNameAsync(allDiseasesModel.SelectedDisease)).First();

            string identificationNum = await UpdateHealthCard(new HealthCardUpdateModel { DiseaseId = disease.Id });
            return RedirectToAction("PatientAskForGetInfoDoctor", "Patient", new { identificationNumber = identificationNum });
        }

        private async Task<Guid> MatchDiseaseAndSypthoms(Guid diseaseGuid, List<Guid> existingSympthomGuids)
        {
            Guid id = Guid.Empty;
            foreach (var guid in existingSympthomGuids)
            {
                DiseaseToSympthomDto diseaseToSympthom = new DiseaseToSympthomDto { DiseaseDto = await DiseaseFacade.GetDiseaseAsync(diseaseGuid), SympthomDto = await SympthomFacade.GetSympthomAsync(guid) };
                id = await DiseaseAndSympthomFacade.Create(diseaseToSympthom);
            }
            return id;
        }

        private async Task<string> UpdateHealthCard(HealthCardUpdateModel healthCardUpdateModel)
        {
            var identificationNum = Request.QueryString["identificationNumber"];
            var patientToUpdate = await PatientFacade.GetPatientByIdentificationNumberAsync(identificationNum);
            var healthCard = await HealthCardFacade.GetHealthCardAsync(patientToUpdate.Id);

            if (healthCard == null)
            {
                var healthCardDto = new HealthCardDto { Id = patientToUpdate.Id , LastUpdate = DateTime.Now};
                await HealthCardFacade.CreateHealthCardAsync(healthCardDto);
                healthCard = await HealthCardFacade.GetHealthCardAsync(patientToUpdate.Id);
            }

            if (healthCardUpdateModel.BloodType != HospitalISDBContext.Enums.BloodType.Unknown)
            {
                healthCard.BloodType = healthCardUpdateModel.BloodType;
            }

            if (healthCardUpdateModel.DiseaseId != Guid.Empty)
            {
                await MatchDiseaseAndHealthCard(await DiseaseFacade.GetDiseaseAsync(healthCardUpdateModel.DiseaseId), healthCard);
            }

            healthCard.LastUpdate = DateTime.Now;
            await HealthCardFacade.EditHealthCardAsync(healthCard);

            return identificationNum;
        }

        private async Task<Guid> MatchDiseaseAndHealthCard(DiseaseDto diseaseDto, HealthCardDto healthCard)
        {
            var diseaseToHealthCard = new DiseaseToHealthCardDto { DiseaseDto = diseaseDto, HealthCardDto = healthCard };
            return await DiseaseToHealthCardFacade.Create(diseaseToHealthCard);
        }

        public ActionResult UpdateBloodType()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBloodType(HealthCardUpdateBloodTypeModel healthCardUpdateBloodTypeModel)
        {
            string identificationNum = await UpdateHealthCard(new HealthCardUpdateModel { BloodType = healthCardUpdateBloodTypeModel.BloodType });
            return RedirectToAction("PatientAskForGetInfoDoctor", "Patient", new { identificationNumber = identificationNum });
        }

    }
}