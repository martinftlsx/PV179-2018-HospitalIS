using System;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.Facades.Common;
using HospitalIS.BusinessLayer.Services.Patients;
using HospitalIS.BusinessLayer.Services.HealthCards;
using HospitalIS.Infrastructure.UnitOfWork_;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalISDBContext.Enums;

namespace HospitalIS.BusinessLayer.Facades
{
    public class PatientFacade : FacadeBase
    {
        private readonly IPatientService patientService;

        public PatientFacade(IUnitOfWorkProvider unitOfWorkProvider, IPatientService patientService) : base(unitOfWorkProvider)
        {
            this.patientService = patientService;
        }

        public async Task<PatientDto> GetPatientAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var patient = await patientService.GetAsync(id);
                return patient;
            }
        }

        public async Task<IEnumerable<PatientDto>> GetPatientByNameAsync(string name)
        {
            using (UnitOfWorkProvider.Create())
            {
                var patients = await patientService.GetPatientByNameAsync(name);
                return patients;
            }
        }

        public async Task<PatientDto> GetPatientByIdentificationNumberAsync(string identificiationNumber)
        {
            using (UnitOfWorkProvider.Create())
            {
                var patient = await patientService.GetPatientByIdentificationNumber(identificiationNumber);
                return patient;
            }
        }

        public async Task<QueryResultDto<PatientDto, PatientFilterDto>> GetAllPatientsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await patientService.ListAllAsync();
            }
        }

        public async Task<IEnumerable<DoctorDto>> GetDoctorsForPatientAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var doctors = await patientService.GetDoctorsForPatientAsync(id);
                return doctors;
            }
        }

        public async Task<Guid> CreatePatientAsync(PatientDto patient)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await patientService.GetPatientByUsernameAsync(patient.Username)) != null)
                {
                    throw new ArgumentException();
                }
                var patientId = patientService.Create(patient);
                await uow.Commit();
                return patientId;
            }
        }

        public async Task<bool> EditPatientAsync(PatientDto patient)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await patientService.GetAsync(patient.Id, false)) == null)
                {
                    return false;
                }
                await patientService.Update(patient);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeletePatientAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await patientService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                patientService.DeleteProduct(id);
                await uow.Commit();
                return true;
            }
        }

        public async Task<HealthCardDto> GetPatientWithHealthCardAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                return await patientService.GetHealthCardForPatientAsync(id);
            }
        }

        public async Task<PatientDto> GetPatientByUsernameAsync(string username)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                return await patientService.GetPatientByUsernameAsync(username);
            }
        }

        public async Task<Guid> RegisterPatient(PatientDto patientDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var id = patientService.RegisterPatientAsync(patientDto);
                await uow.Commit();
                return id;
            }
        }

        public async Task<(bool success, AccessRights roles)> Login(string username, string password)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await patientService.AuthorizePatientAsync(username, password);
            }
        }
    }
}
