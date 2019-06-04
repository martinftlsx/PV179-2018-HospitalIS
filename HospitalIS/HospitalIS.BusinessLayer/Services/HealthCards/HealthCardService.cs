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
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.Services.HealthCards
{
    public class HealthCardService : CrudQueryServiceBase<HealthCard, HealthCardDto, HealthCardFilterDto>, IHealthCardService
    {
        public HealthCardService(IMapper mapper, IRepository<HealthCard> healthCardRepository, QueryObjectBase<HealthCardDto, HealthCard, HealthCardFilterDto, IQuery<HealthCard>> healthCardListQuery)
           : base(mapper, healthCardRepository, healthCardListQuery) { }

        protected override async Task<HealthCard> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(HealthCard.Patient), nameof(HealthCard.DiseaseToHealthCard));
        }

        public async Task<HealthCardDto> GetHealthCardForPatientAsync(Guid patientId)
        {
            var healthCard = await Repository.GetAsync(patientId); // because primary key of healthCard is also foreign key to table Patients
            return Mapper.Map<HealthCardDto>(healthCard);
        }

        public override Guid Create(HealthCardDto entityDto)
        {
            var healthCard = Mapper.Map<HealthCard>(entityDto);
            var id = Repository.CreateWithoutId(healthCard);
            return id;
        }

        public async Task<IEnumerable<DiseaseDto>> GetDiseasesForHealthCard(Guid healthCardId)
        {
            var diseases = new List<DiseaseDto>();
            var healthCard = await GetWithIncludesAsync(healthCardId);

            foreach (var healthCardDisease in healthCard.DiseaseToHealthCard)
            {
                diseases.Add(Mapper.Map<DiseaseDto>(healthCardDisease.Disease));
            }
            return diseases;
        }
    }
}
