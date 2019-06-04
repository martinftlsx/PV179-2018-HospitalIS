using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.Facades.Common;
using HospitalIS.BusinessLayer.Services.Diseases;
using HospitalIS.BusinessLayer.Services.DiseaseToSympthom;
using HospitalIS.BusinessLayer.Services.Sympthoms;
using HospitalIS.Infrastructure.UnitOfWork_;
using HospitalISDBContext.Entities;
using HospitalISInfrastructureEntityFramework.UnitOfWork;

namespace HospitalIS.BusinessLayer.Facades
{
    public class DiseaseAndSympthomFacade : FacadeBase
    {
        private IDiseaseToSympthomService diseaseToSympthomService;

        public DiseaseAndSympthomFacade(IUnitOfWorkProvider unitOfWorkProvider, IDiseaseToSympthomService diseaseToSympthomService) : base(unitOfWorkProvider)
        {
            this.diseaseToSympthomService = diseaseToSympthomService;
        }

        public async Task<Guid> Create(DiseaseToSympthomDto diseaseToSympthomDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var guid = diseaseToSympthomService.Create(diseaseToSympthomDto.DiseaseDto.Id, diseaseToSympthomDto.SympthomDto.Id);
                await uow.Commit();
                return guid;
            }
        }
    }
}
