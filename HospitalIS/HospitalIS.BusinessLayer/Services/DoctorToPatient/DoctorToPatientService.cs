using AutoMapper;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.QueryObjects.Common;
using HospitalIS.BusinessLayer.Services.Common;
using HospitalIS.Infrastructure;
using HospitalIS.Infrastructure.Query;
using HospitalISDBContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.Services.DoctorToPatient
{
    public class DoctorToPatientService : CrudQueryServiceBase<HospitalISDBContext.Entities.DoctorToPatient, DoctorToPatientDto, DoctorToPatientFilterDto>, IDoctorToPatientService
    {        
        public DoctorToPatientService(IMapper mapper, IRepository<Doctor> doctorRepository, IRepository<Patient> patientRepository, 
            IRepository<HospitalISDBContext.Entities.DoctorToPatient> doctorToPatientRepository,
            QueryObjectBase<DoctorToPatientDto, HospitalISDBContext.Entities.DoctorToPatient, DoctorToPatientFilterDto, IQuery<HospitalISDBContext.Entities.DoctorToPatient>> doctorToPatientQuery) 
            : base(mapper, doctorToPatientRepository, doctorToPatientQuery)
        {
        }

        public Guid Create(Guid doctorGuid, Guid patientGuid)
        {
            HospitalISDBContext.Entities.DoctorToPatient doctorToPatient = new HospitalISDBContext.Entities.DoctorToPatient
            {
                DoctorId = doctorGuid,
                PatientId = patientGuid
            };
            return Repository.Create(doctorToPatient);
        }

        public async Task<Guid> FindJoiningRelationshipIdAsync(Guid doctorId, Guid patientId)
        {
            var queryResult = await Query.ExecuteQuery(new DoctorToPatientFilterDto { DoctorId = doctorId, PatientId = patientId });
            if (queryResult.Items.Count() == 0)
            {
                return Guid.Empty;
            }
            return queryResult.Items.Single().Id;
        }

        protected override async Task<HospitalISDBContext.Entities.DoctorToPatient> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Patient), nameof(Doctor));
        }
    }
}
