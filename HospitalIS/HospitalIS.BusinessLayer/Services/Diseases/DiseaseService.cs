using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.QueryObjects.Common;
using HospitalIS.BusinessLayer.Services.Common;
using HospitalIS.Infrastructure;
using HospitalIS.Infrastructure.Query;
using HospitalISDBContext.Entities;

namespace HospitalIS.BusinessLayer.Services.Diseases
{
    public class DiseaseService : CrudQueryServiceBase<Disease, DiseaseDto, DiseaseFilterDto>, IDiseaseService
    {
        public DiseaseService(IMapper mapper, IRepository<Disease> diseaseRepository, QueryObjectBase<DiseaseDto, Disease, DiseaseFilterDto, IQuery<Disease>> diseaseListQuery)
            : base(mapper, diseaseRepository, diseaseListQuery) { }

        protected override async Task<Disease> GetWithIncludesAsync(Guid entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Disease.DiseaseToSympthoms));
        }

        public async Task<IEnumerable<DiseaseDto>> GetDiseaseByNameAsync(string name)
        {
            var queryResult = await Query.ExecuteQuery(new DiseaseFilterDto { DiseaseName = name });
            return queryResult.Items.Select(disease => disease).ToList();
        }

        public async Task<IEnumerable<string>> GetSympthomsForDiseaseAsync(Guid id)
        {
            var list = new List<string>();
            var disease = await GetWithIncludesAsync(id);
            
            foreach (var sympthomToDisease in disease.DiseaseToSympthoms)
            {
                list.Add(sympthomToDisease.Sympthom.Name);
            }
            return list;
        }
    }
}
