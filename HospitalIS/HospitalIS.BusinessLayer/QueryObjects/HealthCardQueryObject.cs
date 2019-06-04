using AutoMapper;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.QueryObjects.Common;
using HospitalIS.Infrastructure.Query;
using HospitalISDBContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.QueryObjects
{
    public class HealthCardQueryObject : QueryObjectBase<HealthCardDto, HealthCard, HealthCardFilterDto, IQuery<HealthCard>>
    {
        public HealthCardQueryObject(IMapper mapper, IQuery<HealthCard> query) : base(mapper, query) { }

        protected override IQuery<HealthCard> ApplyWhereClause(IQuery<HealthCard> query, HealthCardFilterDto filter)
        {
            return query;
        }
    }
}
