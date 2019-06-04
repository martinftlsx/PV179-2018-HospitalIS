using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.Facades;
using HospitalIS.WebApplication.Models;

namespace HospitalIS.WebApplication.Controllers
{
    public class DoctorController : Controller
    {
        public DoctorFacade DoctorFacade { get; set; }

        public PatientFacade PatientFacade { get; set; }

        public async Task<ActionResult> Index()
        {
            List<DoctorInfoModel> modelDocs = new List<DoctorInfoModel>();
            var allDoctors = await DoctorFacade.GetAllDoctorsAsync();
            foreach (var doc in allDoctors.Items)
            {
                modelDocs.Add(new DoctorInfoModel{ Username = doc.Username, Name = doc.Name, Surname = doc.Surname, Specialization = doc.Specialization.ToString()});
            }

            return View(modelDocs);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(DoctorCreateModel doctorCreateModel)
        {
            if (!ModelState.IsValid) return View(doctorCreateModel);
            DoctorDto doctorDto = new DoctorDto
            {
                Username = doctorCreateModel.Username,
                Password = doctorCreateModel.Password,
                Specialization = doctorCreateModel.Specialization,
                Name = doctorCreateModel.Name,
                Surname = doctorCreateModel.Surname
            };

            if (await PatientFacade.GetPatientByUsernameAsync(doctorCreateModel.Username) != null || 
                await DoctorFacade.GetDoctorByUsernameAsync(doctorCreateModel.Username) != null)
            {
                ModelState.AddModelError("Username", "Account with that username already exists");
                return View(doctorCreateModel);
            }
            await DoctorFacade.RegisterDoctor(doctorDto);
            return RedirectToAction("Login", "Account");            
        }

        public async Task<ActionResult> Update()
        {
            var doctorData = await DoctorFacade.GetDoctorByUsernameAsync(HttpContext.User.Identity.Name);
            DoctorUpdateModel doctorUpdateModel = new DoctorUpdateModel
            {
                Name = doctorData.Name,
                Surname = doctorData.Surname
            };
            return View(doctorUpdateModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(DoctorUpdateModel doctorUpdateModel)
        {
            if (!ModelState.IsValid) return View(doctorUpdateModel);
            doctorUpdateModel.Username = HttpContext.User.Identity.Name;
            var doctorToUpdate = await DoctorFacade.GetDoctorByUsernameAsync(doctorUpdateModel.Username);
            if (doctorToUpdate == null)
            {
                ModelState.AddModelError("Username", "Account does not exist");
                return View(doctorUpdateModel);
            }
            DoctorDto doctorDto = new DoctorDto
            {
                Username = HttpContext.User.Identity.Name,
                Id = doctorToUpdate.Id,
                Specialization = doctorToUpdate.Specialization,
                Name = doctorUpdateModel.Name,
                Surname = doctorUpdateModel.Surname
            };
            await DoctorFacade.EditDoctorAsync(doctorDto);
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult>GetPatientsForDoctor()
        {
            var username = Request.QueryString["Username"];
            var doctor = await DoctorFacade.GetDoctorByUsernameAsync(username);
            var patients = await DoctorFacade.GetPatientsForDoctorAsync(doctor.Id);
            return View(patients);
        }
    }
}