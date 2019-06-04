using AutoMapper;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.QueryObjects.Common;
using HospitalIS.BusinessLayer.Services.Common;
using HospitalIS.Infrastructure;
using HospitalIS.Infrastructure.Query;
using HospitalISDBContext.Entities;
using HospitalISDBContext.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.Services.Patients
{
    public class PatientService : CrudQueryServiceBase<Patient, PatientDto, PatientFilterDto>, IPatientService
    {
        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;

        public PatientService(IMapper mapper, IRepository<Patient> patientRepository,
            QueryObjectBase<PatientDto, Patient, PatientFilterDto, IQuery<Patient>> patientListQuery)
            : base(mapper, patientRepository, patientListQuery)
        {
        }

        protected override async Task<Patient> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Patient.DoctorToPatients), nameof(Patient.HealthCard));
        }

        public async Task<PatientDto> GetPatientByUsernameAsync(string username)
        {
            var queryResult = await Query.ExecuteQuery(new PatientFilterDto { Username = username });
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<IEnumerable<PatientDto>> GetPatientByNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new PatientFilterDto { FullName = name });
            return queryResult.Items.Select(patient => patient).ToList();
        }

        public async Task<IEnumerable<DoctorDto>> GetDoctorsForPatientAsync(Guid id)
        {
            var doctors = new List<DoctorDto>();
            var patient = await GetWithIncludesAsync(id);

            foreach (var doctorToPatient in patient.DoctorToPatients)
            {
                doctors.Add(Mapper.Map<DoctorDto>(doctorToPatient.Doctor));
            }

            return doctors;
        }

        public async Task<PatientDto> GetPatientByIdentificationNumber(string identificationNumber)
        {
            var queryResult = await Query.ExecuteQuery(new PatientFilterDto { IdentificationNumber = identificationNumber });
            var patient = queryResult.Items.SingleOrDefault();
            if (patient != null)
            {
                patient.Doctors = (ICollection<DoctorDto>) await GetDoctorsForPatientAsync(patient.Id);
                return patient;
            }

            return null;
        }

        public async Task<HealthCardDto> GetHealthCardForPatientAsync(Guid id)
        {
            var patient = await GetWithIncludesAsync(id);
            var healthCard = Mapper.Map<HealthCardDto>(patient.HealthCard);
            return healthCard;
        }

        public Guid RegisterPatientAsync(PatientDto patientDto)
        {
            var patient = Mapper.Map<Patient>(patientDto);

            var password = CreateHash(patientDto.Password);
            patient.PasswordHash = password.Item1;
            patient.PasswordSalt = password.Item2;

            Repository.Create(patient);

            return patient.Id;
        }

        public async Task<(bool success, AccessRights roles)> AuthorizePatientAsync(string username, string password)
        {
            var patientQueryResult = await Query.ExecuteQuery(new PatientFilterDto { Username = username });
            var patient = patientQueryResult.Items.SingleOrDefault();

            var succ = patient != null && VerifyHashedPassword(patient.PasswordHash, patient.PasswordSalt, password);
            var roles = AccessRights.Patient;
            return (succ, roles);
        }

        private async Task<bool> GetIfPatientExistsAsync(string username)
        {
            var queryResult = await Query.ExecuteQuery(new PatientFilterDto { Username = username });
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
