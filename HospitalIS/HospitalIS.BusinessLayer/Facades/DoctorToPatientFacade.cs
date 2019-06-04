using HospitalIS.BusinessLayer.Facades.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.Services.DoctorToPatient;
using HospitalIS.Infrastructure.UnitOfWork_;

namespace HospitalIS.BusinessLayer.Facades
{
    public class DoctorToPatientFacade : FacadeBase
    {
        private IDoctorToPatientService doctorToPatientService;
        public DoctorToPatientFacade(IUnitOfWorkProvider unitOfWorkProvider, IDoctorToPatientService doctorToPatientService) : base(unitOfWorkProvider)
        {
            this.doctorToPatientService = doctorToPatientService;
        }

        public async Task<Guid> Create(DoctorToPatientDto doctorToPatientDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var guid = doctorToPatientService.Create(doctorToPatientDto.DoctorDto.Id, doctorToPatientDto.PatientDto.Id);
                await uow.Commit();
                return guid;
            }
        }

        public async Task<Guid> FindJoiningRelationshipId(Guid doctorId, Guid patientId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var guid = await doctorToPatientService.FindJoiningRelationshipIdAsync(doctorId, patientId);
                return guid;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (await doctorToPatientService.GetAsync(id, false) == null)
                {
                    return false;
                }
                doctorToPatientService.DeleteProduct(id);
                await uow.Commit();
                return true;
            }
        }
    }
}
