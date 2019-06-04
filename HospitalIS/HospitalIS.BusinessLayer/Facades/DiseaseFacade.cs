using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.Facades.Common;
using HospitalIS.BusinessLayer.Services.Diseases;
using HospitalIS.Infrastructure.UnitOfWork_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.Facades
{
    public class DiseaseFacade : FacadeBase
    {
        private readonly IDiseaseService diseaseService;

        public DiseaseFacade(IUnitOfWorkProvider unitOfWorkProvider, IDiseaseService diseaseService) : base(unitOfWorkProvider)
        {
            this.diseaseService = diseaseService;
        }

        #region Diseases

        public async Task<DiseaseDto> GetDiseaseAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var disease = await diseaseService.GetAsync(id);
                return disease;
            }
        }

        public async Task<IEnumerable<DiseaseDto>> GetDiseaseByNameAsync(string name)
        {
            using (UnitOfWorkProvider.Create())
            {
                var diseases = await diseaseService.GetDiseaseByNameAsync(name);
                return diseases;
            }
        }

        public async Task<IEnumerable<string>> GetDiseasesBySympthomsAsync(string[] sympthomNames)
        {
            List<string> diseases = new List<string>();
            using (UnitOfWorkProvider.Create())
            {
                var allDiseases = await diseaseService.ListAllAsync();
                foreach (var disease in allDiseases.Items)
                {
                    var sympthomsForDisease = await diseaseService.GetSympthomsForDiseaseAsync(disease.Id);
                    if (sympthomNames.All(sympthom => sympthomsForDisease.Contains(sympthom)))
                    {
                        diseases.Add(disease.Name);
                    }
                }
                return diseases;
            }
        }

        public async Task<QueryResultDto<DiseaseDto, DiseaseFilterDto>> GetAllDiseasesAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await diseaseService.ListAllAsync();
            }
        }

        public async Task<IEnumerable<string>> GetSympthomsForDiseaseAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var sympthoms = await diseaseService.GetSympthomsForDiseaseAsync(id);
                return sympthoms;
            }
        }

        public async Task<Guid> CreateDiseaseAsync(DiseaseDto disease)
        { // need to create Sympthom as well?
            using (var uow = UnitOfWorkProvider.Create())
            {
                var diseaseId = diseaseService.Create(disease);
                await uow.Commit();
                return diseaseId;
            }
        }

        public async Task<bool> EditDiseaseAsync(DiseaseDto diseaseDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await diseaseService.GetAsync(diseaseDto.Id, false)) == null)
                {
                    return false;
                }
                await diseaseService.Update(diseaseDto);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteDiseaseAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await diseaseService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                diseaseService.DeleteProduct(id);
                await uow.Commit();
                return true;
            }
        }

        #endregion
    }
}
