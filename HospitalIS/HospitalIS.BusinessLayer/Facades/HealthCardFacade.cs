using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.Facades.Common;
using HospitalIS.BusinessLayer.Services.HealthCards;
using HospitalIS.Infrastructure.UnitOfWork_;

namespace HospitalIS.BusinessLayer.Facades
{
    public class HealthCardFacade : FacadeBase
    {
        private readonly IHealthCardService healthCardService;

        public HealthCardFacade(IUnitOfWorkProvider unitOfWorkProvider, IHealthCardService healthCardService) : base(unitOfWorkProvider)
        {
            this.healthCardService = healthCardService;
        }

        public async Task<HealthCardDto> GetHealthCardAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var healthCard = await healthCardService.GetAsync(id);
                return healthCard;
            }
        }

        public async Task<QueryResultDto<HealthCardDto, HealthCardFilterDto>> GetAllHealthCardsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await healthCardService.ListAllAsync();
            }
        }

        public async Task<HealthCardDto> GetHealthCardForPatientAsync(Guid patientId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var healthCard = await healthCardService.GetHealthCardForPatientAsync(patientId);
                return healthCard;
            }
        }

        public async Task<IEnumerable<DiseaseDto>> GetDiseasesForHealthCard(Guid healthCardId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                return await healthCardService.GetDiseasesForHealthCard(healthCardId);
            }
        }

        public async Task<Guid> CreateHealthCardAsync(HealthCardDto healthCard)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var healthCardId = healthCardService.Create(healthCard);
                await uow.Commit();
                return healthCardId;
            }
        }

        public async Task<bool> EditHealthCardAsync(HealthCardDto healthCard)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await healthCardService.GetAsync(healthCard.Id, true)) == null)
                {
                    return false;
                }
                await healthCardService.Update(healthCard);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteHealthCardAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await healthCardService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                healthCardService.DeleteProduct(id);
                await uow.Commit();
                return true;
            }
        }
    }
}
