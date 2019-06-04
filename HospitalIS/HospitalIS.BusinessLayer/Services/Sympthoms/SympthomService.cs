using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.QueryObjects.Common;
using HospitalIS.BusinessLayer.Services.Common;
using HospitalIS.Infrastructure;
using HospitalIS.Infrastructure.Query;
using HospitalISDBContext.Entities;

namespace HospitalIS.BusinessLayer.Services.Sympthoms
{
    public class SympthomService : CrudQueryServiceBase<Sympthom, SympthomDto, SympthomFilterDto>, ISympthomService
    {
        public SympthomService(IMapper mapper, IRepository<Sympthom> sympthomRepository, QueryObjectBase<SympthomDto, Sympthom, SympthomFilterDto, IQuery<Sympthom>> sympthomQuery) 
            : base(mapper, sympthomRepository, sympthomQuery) { }

        protected override async Task<Sympthom> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Sympthom.DiseaseToSympthoms));
        }

        public async Task<IEnumerable<SympthomDto>> GetSympthomByNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new SympthomFilterDto { Name = name });
            return queryResult.Items.Select(sympthom => sympthom).ToList();
        }
    }
}
