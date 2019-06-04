using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.Core.Internal;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.Facades;
using HospitalISDBContext.Enums;
using HospitalWebAPI.Models;

namespace HospitalWebAPI.Controllers
{
    public class DoctorController : ApiController
    {
        
        public DoctorFacade DoctorFacade { get; set; }

        [HttpPost, Route("api/Doctor/Post")]
        public async Task<string> Post([FromBody]CreateDoctorModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        
            var doctorDto = new DoctorDto();
            doctorDto.Username = model.Username;
            doctorDto.Password = model.Password;
            doctorDto.Name = model.Name;
            doctorDto.Surname = model.Surname;
            doctorDto.PatientDtos = new HashSet<PatientDto>();
            doctorDto.Specialization = (Specialization)Enum.Parse(typeof(Specialization), model.Specialization);
            var doctorId = await DoctorFacade.RegisterDoctor(doctorDto);
            if (doctorId == Guid.Empty)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return $"Created DOCTOR with username = {doctorDto.Username}";
        }

        [HttpGet, Route("api/Doctor/Get")]
        public async Task<GetDoctorModel> Get([FromUri]string username)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var doctorDto = await DoctorFacade.GetDoctorByUsernameAsync(username);
            if (doctorDto == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            var getDoctorModel = new GetDoctorModel();
            getDoctorModel.Name = doctorDto.Name;
            getDoctorModel.Surname = doctorDto.Surname;
            getDoctorModel.Specialization = doctorDto.Specialization.ToString();
            return getDoctorModel;
        }

        [HttpPost, Route("api/Doctor/Edit")]
        public async Task<string> Edit([FromBody] EditDoctorModel editDoctorModel)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var doctor = await DoctorFacade.GetDoctorByUsernameAsync(editDoctorModel.Username);
            if (doctor == null) throw new HttpResponseException(HttpStatusCode.NotModified); 
            var doctorDto = doctor;
            doctorDto.Id = doctor.Id;
            doctorDto.Name = editDoctorModel.Name.IsNullOrEmpty() ? null : editDoctorModel.Name;
            doctorDto.Surname = editDoctorModel.Surname.IsNullOrEmpty() ? null : editDoctorModel.Surname;
            if (editDoctorModel.Specialization != null)
            {
                Specialization spec;
                var succ = Specialization.TryParse(editDoctorModel.Specialization, out spec);
                if (!succ) throw new HttpResponseException(HttpStatusCode.NotModified);
                doctorDto.Specialization = spec;
            }
            var succ1 = await DoctorFacade.EditDoctorAsync(doctorDto);
            if (!succ1) throw new HttpResponseException(HttpStatusCode.NotModified);
            return $"Edited DOCTOR with username = {editDoctorModel.Username}";
        }

    }
}
