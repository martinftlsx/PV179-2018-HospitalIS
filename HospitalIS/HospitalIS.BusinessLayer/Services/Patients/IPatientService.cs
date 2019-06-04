using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalISDBContext.Enums;

namespace HospitalIS.BusinessLayer.Services.Patients
{
    public interface IPatientService
    {
        Task<PatientDto> GetPatientByUsernameAsync(string username);

        Task<IEnumerable<PatientDto>> GetPatientByNameAsync(string name);

        Task<IEnumerable<DoctorDto>> GetDoctorsForPatientAsync(Guid id);

        Task<HealthCardDto> GetHealthCardForPatientAsync(Guid id);
        
        Task<PatientDto> GetPatientByIdentificationNumber(string identificationNumber);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(PatientDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(PatientDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void DeleteProduct(Guid entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<PatientDto, PatientFilterDto>> ListAllAsync();

        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<PatientDto> GetAsync(Guid entityId, bool withIncludes = true);

        Guid RegisterPatientAsync(PatientDto patientDto);
        Task<(bool success, AccessRights roles)> AuthorizePatientAsync(string username, string password);

    }
}
