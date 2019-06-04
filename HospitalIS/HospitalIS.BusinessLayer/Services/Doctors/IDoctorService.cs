using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalISDBContext.Enums;

namespace HospitalIS.BusinessLayer.Services.Doctors
{
    public interface IDoctorService
    {
        Task<DoctorDto> GetDoctorByUsernameAsync(string username);

        Task<IEnumerable<DoctorDto>> GetDoctorByNameAsync(string name);

        Task<IEnumerable<DoctorDto>> GetDoctorsBySpecializationAsync(string specialization);

        Task<IEnumerable<PatientDto>> GetPatientsForDoctorAsync(Guid id);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Guid Create(DoctorDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(DoctorDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void DeleteProduct(Guid entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<DoctorDto, DoctorFilterDto>> ListAllAsync();

        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<DoctorDto> GetAsync(Guid entityId, bool withIncludes = true);

        Guid RegisterDoctorAsync(DoctorDto doctorDto);
        Task<(bool success, AccessRights roles)> AuthorizeDoctorAsync(string username, string password);
    }
}
