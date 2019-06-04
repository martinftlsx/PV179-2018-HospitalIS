using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Castle.Core.Internal;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.Facades;
using HospitalIS.WebApplication.Models;
using Microsoft.Ajax.Utilities;

namespace HospitalIS.WebApplication.Controllers
{
    public class DoctorToPatientController : Controller
    {
        public DoctorFacade DoctorFacade { get; set; }
        public PatientFacade PatientFacade { get; set; }
        public DoctorToPatientFacade DoctorToPatientFacade { get; set; }

        public ActionResult AddDoctorToPatient()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddDoctorToPatient(AddDoctorToPatientModel model)
        {
            if (model.Name.IsNullOrEmpty())
            {
                ModelState.AddModelError("Name", "Field cannot be empty");
                return View(model);
            }
            var patients = (await PatientFacade.GetAllPatientsAsync())
                .Items
                .Where(patient => (patient.Name + " " + patient.Surname).Contains(model.Name))
                .ToList();
            return View("ShowPatientsForName", patients);
        }

        public async Task<ActionResult> AddClickedPatient()
        {
            var username = Request.QueryString["Username"];
            var foundPatient = await PatientFacade.GetPatientByUsernameAsync(username);
            var loggedDoctor = await DoctorFacade.GetDoctorByUsernameAsync(HttpContext.User.Identity.Name);
            var relationshipGuid = await DoctorToPatientFacade.FindJoiningRelationshipId(loggedDoctor.Id, foundPatient.Id);
            if (relationshipGuid == Guid.Empty)
            {
                var doctorToPatientDto = new DoctorToPatientDto { DoctorDto = loggedDoctor, PatientDto = foundPatient };
                await DoctorToPatientFacade.Create(doctorToPatientDto);
            }
            return RedirectToAction("GetPatientsForDoctor", "Doctor", new { Username = loggedDoctor.Username });
        }

        public async Task<ActionResult> UnmatchDoctorAndPatient(string identificationNumber)
        {
            var patientDto = await PatientFacade.GetPatientByIdentificationNumberAsync(identificationNumber);
            var doctorDto = await DoctorFacade.GetDoctorByUsernameAsync(HttpContext.User.Identity.Name);

            var rowId = await DoctorToPatientFacade.FindJoiningRelationshipId(doctorDto.Id, patientDto.Id);
            await DoctorToPatientFacade.Delete(rowId);
            return RedirectToAction("GetPatientsForDoctor", "Doctor", new { Username = HttpContext.User.Identity.Name });
        }

        public async Task<JsonResult> Search(string query)
        {
            var patients = (await PatientFacade.GetAllPatientsAsync()).Items;
            var models = patients.Select(patient => new AddDoctorToPatientModel
                {Name = patient.Name + " " + patient.Surname}).Where(patient => patient.Name.Contains(query));
            return new JsonResult{Data = models, JsonRequestBehavior = JsonRequestBehavior.AllowGet};

        }
    }
}