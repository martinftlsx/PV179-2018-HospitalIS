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
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HospitalISDBContext.Enums;

namespace HospitalIS.BusinessLayer.Services.Doctors
{
    public class DoctorService : CrudQueryServiceBase<Doctor, DoctorDto, DoctorFilterDto>, IDoctorService
    {
        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;

        public DoctorService(IMapper mapper, IRepository<Doctor> doctorRepository,
            QueryObjectBase<DoctorDto, Doctor, DoctorFilterDto, IQuery<Doctor>> doctorListQuery)
            : base(mapper, doctorRepository, doctorListQuery)
        {
        }

        protected override async Task<Doctor> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Doctor.DoctorToPatients));
        }

        public async Task<DoctorDto> GetDoctorByUsernameAsync(string username)
        {
            var queryResult = await Query.ExecuteQuery(new DoctorFilterDto { Username = username });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<IEnumerable<DoctorDto>> GetDoctorByNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new DoctorFilterDto { FullName = name });
            return queryResult.Items.Select(doctor => doctor).ToList();
        }

        public async Task<IEnumerable<DoctorDto>> GetDoctorsBySpecializationAsync(string specialization)
        {
            var queryResult = await Query.ExecuteQuery(new DoctorFilterDto { Specialization = specialization });
            return queryResult.Items.Select(doctor => doctor).ToList();
        }

        public async Task<IEnumerable<PatientDto>> GetPatientsForDoctorAsync(Guid id)
        {
            var patients = new List<PatientDto>();
            var doctor = await GetWithIncludesAsync(id);

            foreach (var doctorToPatient in doctor.DoctorToPatients)
            {
                patients.Add(Mapper.Map<PatientDto>(doctorToPatient.Patient));
            }

            return patients;
        }

        public  Guid RegisterDoctorAsync(DoctorDto doctorDto)
        {
            var doctor = Mapper.Map<Doctor>(doctorDto);

            var password = CreateHash(doctorDto.Password);
            doctor.PasswordHash = password.Item1;
            doctor.PasswordSalt = password.Item2;

            Repository.Create(doctor);

            return doctor.Id;
        }

        public async Task<(bool success, AccessRights roles)> AuthorizeDoctorAsync(string username, string password)
        {
            var doctorQueryResult = await Query.ExecuteQuery(new DoctorFilterDto { Username = username });
            var doctor = doctorQueryResult.Items.SingleOrDefault();

            var succ = doctor != null && VerifyHashedPassword(doctor.PasswordHash, doctor.PasswordSalt, password);
            var roles = AccessRights.Doctor;
            return (succ, roles);
        }

        private async Task<bool> GetIfDoctorExistsAsync(string username)
        {
            var queryResult = await Query.ExecuteQuery(new DoctorFilterDto { Username = username });
            return (queryResult.Items.Count() == 1);
        }

        private bool VerifyHashedPassword(string hashedPassword, string salt, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            var saltBytes = Convert.FromBase64String(salt);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, PBKDF2IterCount))
            {
                var generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                return hashedPasswordBytes.SequenceEqual(generatedSubkey);
            }
        }

        private Tuple<string, string> CreateHash(string password)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                return Tuple.Create(Convert.ToBase64String(subkey), Convert.ToBase64String(salt));
            }
        }
    }
}
