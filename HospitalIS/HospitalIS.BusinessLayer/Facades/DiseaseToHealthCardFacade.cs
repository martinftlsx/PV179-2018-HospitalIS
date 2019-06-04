using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.Facades.Common;
using HospitalIS.BusinessLayer.Services.DiseaseToHealthCard;
using HospitalIS.Infrastructure.UnitOfWork_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.Facades
{
    public class DiseaseToHealthCardFacade : FacadeBase
    {
        private readonly IDiseaseToHealthCardService diseaseToHealthCardService;

        public DiseaseToHealthCardFacade(IUnitOfWorkProvider unitOfWorkProvider, IDiseaseToHealthCardService diseaseToHealthCardService) : base(unitOfWorkProvider)
        {
            this.diseaseToHealthCardService = diseaseToHealthCardService;
        }

        public async Task<Guid> Create(DiseaseToHealthCardDto diseaseToHealthCardDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var guid = diseaseToHealthCardService.Create(diseaseToHealthCardDto.DiseaseDto.Id, diseaseToHealthCardDto.HealthCardDto.Id);
                await uow.Commit();
                return guid;
            }
        }
    }
}
