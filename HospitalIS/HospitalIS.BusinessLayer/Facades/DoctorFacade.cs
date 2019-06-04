using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.Facades.Common;
using HospitalIS.BusinessLayer.Services.Doctors;
using HospitalIS.Infrastructure.UnitOfWork_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalISDBContext.Enums;

namespace HospitalIS.BusinessLayer.Facades
{
    public class DoctorFacade : FacadeBase
    {
        private readonly IDoctorService doctorService;

        public DoctorFacade(IUnitOfWorkProvider unitOfWorkProvider, IDoctorService doctorService) : base(unitOfWorkProvider)
        {
            this.doctorService = doctorService;
        }

        public async Task<DoctorDto> GetDoctorAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var doctor = await doctorService.GetAsync(id);
                return doctor;
            }
        }

        public async Task<IEnumerable<DoctorDto>> GetDoctorByNameAsync(string name)
        {
            using (UnitOfWorkProvider.Create())
            {
                var doctors = await doctorService.GetDoctorByNameAsync(name);
                return doctors;
            }
        }

        public async Task<IEnumerable<DoctorDto>> GetDoctorBySpecializationAsync(string specialization)
        {
            using (UnitOfWorkProvider.Create())
            {
                var doctors = await doctorService.GetDoctorsBySpecializationAsync(specialization);
                return doctors;
            }
        }

        public async Task<QueryResultDto<DoctorDto, DoctorFilterDto>> GetAllDoctorsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await doctorService.ListAllAsync();
            }
        }

        public async Task<IEnumerable<PatientDto>> GetPatientsForDoctorAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var patients = await doctorService.GetPatientsForDoctorAsync(id);
                return patients;
            }
        }

        public async Task<Guid> CreateDoctorAsync(DoctorDto doctor)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await doctorService.GetDoctorByUsernameAsync(doctor.Username)) != null)
                {
                    throw new ArgumentException();
                }
                var doctorId = doctorService.Create(doctor);
                await uow.Commit();
                return doctorId;
            }
        }

        public async Task<DoctorDto> GetDoctorByUsernameAsync(string username)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                return (await doctorService.GetDoctorByUsernameAsync(username));
            }
        }

        public async Task<bool> EditDoctorAsync(DoctorDto doctor)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await doctorService.GetAsync(doctor.Id, false)) == null)
                {
                    return false;
                }
                await doctorService.Update(doctor);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteDoctorAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await doctorService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                doctorService.DeleteProduct(id);
                await uow.Commit();
                return true;
            }
        }


        public async Task<Guid> RegisterDoctor(DoctorDto doctorDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var id = doctorService.RegisterDoctorAsync(doctorDto);
                await uow.Commit();
                return id;
            }
        }

        public async Task<(bool success, AccessRights roles)> Login(string username, string password)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await doctorService.AuthorizeDoctorAsync(username, password);
            }
        }

    }
}
