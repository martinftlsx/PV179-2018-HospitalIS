using HospitalIS.BusinessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.Services.DoctorToPatient
{
    public interface IDoctorToPatientService
    {
        Guid Create(Guid doctorGuid, Guid patientGuid);

        void DeleteProduct(Guid id);

        Task<Guid> FindJoiningRelationshipIdAsync(Guid doctorId, Guid patientId);

        Task<DoctorToPatientDto> GetAsync(Guid id, bool withIncludes = true);
    }
}
