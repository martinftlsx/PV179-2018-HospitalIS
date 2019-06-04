using AutoMapper;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.QueryObjects.Common;
using HospitalIS.Infrastructure.Query;
using HospitalIS.Infrastructure.Query.Predicates;
using HospitalIS.Infrastructure.Query.Predicates.Operators;
using HospitalISDBContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalIS.BusinessLayer.QueryObjects
{
    public class DiseaseQueryObject : QueryObjectBase<DiseaseDto, Disease, DiseaseFilterDto, IQuery<Disease>>
    {
        public DiseaseQueryObject(IMapper mapper, IQuery<Disease> query):base(mapper, query) { }

        protected override IQuery<Disease> ApplyWhereClause(IQuery<Disease> query, DiseaseFilterDto filter)
        {
            return filter.DiseaseName == null
                ? query
                : query.Where(new SimplePredicate(nameof(Disease.Name), ValueComparingOperator.StringContains, filter.DiseaseName));
        }
    }
}
