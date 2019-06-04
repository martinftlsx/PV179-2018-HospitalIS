using HospitalIS.BusinessLayer.Facades;
using HospitalWebAPI.Models;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalISDBContext.Enums;

namespace HospitalWebAPI.Controllers
{
    public class PatientController : ApiController
    {
        public PatientFacade PatientFacade { get; set; }

        [HttpGet, Route("api/Patient/GetByIdentificationNumber")]
        public async Task<ReturnPatientInformationModel> GetByIdentificationNumber([FromUri]GetPatientInformationModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            PatientDto patientDto = await PatientFacade.GetPatientByIdentificationNumberAsync(model.IdentificationNumber);
            if (patientDto == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ReturnPatientInformationModel patientInfo = new ReturnPatientInformationModel();
            patientInfo.Name = patientDto.Name;
            patientInfo.Surname = patientDto.Surname;
            patientInfo.DateOfBirth = patientDto.DateOfBirth;
            patientInfo.Email = patientDto.Email;
            patientInfo.IdentificationNumber = patientDto.IdentificationNumber;
            patientInfo.ProfileCreationDate = patientDto.ProfileCreationDate;
            patientInfo.Doctors = patientDto.Doctors.Select(dto =>
                new Tuple<string, Specialization>($"{dto.Name} {dto.Surname}", dto.Specialization));
            return patientInfo;
        }
    }
}
