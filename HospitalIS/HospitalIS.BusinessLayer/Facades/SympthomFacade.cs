using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.Facades.Common;
using HospitalIS.BusinessLayer.Services.Sympthoms;
using HospitalIS.Infrastructure.UnitOfWork_;

namespace HospitalIS.BusinessLayer.Facades
{
    public class SympthomFacade : FacadeBase
    {
        private readonly ISympthomService sympthomService;

        public SympthomFacade(IUnitOfWorkProvider unitOfWorkProvider, ISympthomService sympthomService) : base(unitOfWorkProvider)
        {
            this.sympthomService = sympthomService;
        }

        public async Task<SympthomDto> GetSympthomAsync(Guid id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var sympthom = await sympthomService.GetAsync(id);
                return sympthom;
            }
        }

        public async Task<IEnumerable<SympthomDto>> GetSympthomByNameAsync(string name)
        {
            using (UnitOfWorkProvider.Create())
            {
                var sympthoms = await sympthomService.GetSympthomByNameAsync(name);
                return sympthoms;
            }
        }

        public async Task<QueryResultDto<SympthomDto, SympthomFilterDto>> GetAllSympthomsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await sympthomService.ListAllAsync();
            }
        }


        public async Task<Guid> CreateSympthomAsync(SympthomDto sympthom)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var sympthomId = sympthomService.Create(sympthom);
                await uow.Commit();
                return sympthomId;
            }
        }

        public async Task<bool> EditSympthomAsync(SympthomDto sympthomDto)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await sympthomService.GetAsync(sympthomDto.Id, false)) == null)
                {
                    return false;
                }
                await sympthomService.Update(sympthomDto);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteSympthomAsync(Guid id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await sympthomService.GetAsync(id, false)) == null)
                {
                    return false;
                }
                sympthomService.DeleteProduct(id);
                await uow.Commit();
                return true;
            }
        }

    }
}
