using AutoMapper;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.QueryObjects.Common;
using HospitalIS.Infrastructure.Query;
using HospitalIS.Infrastructure.Query.Predicates;
using HospitalIS.Infrastructure.Query.Predicates.Operators;
using HospitalISDBContext.Entities;

namespace HospitalIS.BusinessLayer.QueryObjects
{
    public class SympthomQueryObject : QueryObjectBase<SympthomDto, Sympthom, SympthomFilterDto, IQuery<Sympthom>>
    {
        public SympthomQueryObject(IMapper mapper, IQuery<Sympthom> query) : base(mapper, query) { }

        protected override IQuery<Sympthom> ApplyWhereClause(IQuery<Sympthom> query, SympthomFilterDto filter)
        {
            return string.IsNullOrWhiteSpace(filter.Name)
                ? query
                : query.Where(new SimplePredicate(nameof(Sympthom.Name), ValueComparingOperator.StringContains, filter.Name));
        }
    }
}
