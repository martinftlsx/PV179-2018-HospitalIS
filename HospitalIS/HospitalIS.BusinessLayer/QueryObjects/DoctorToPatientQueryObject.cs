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
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.QueryObjects
{
    public class DoctorToPatientQueryObject : QueryObjectBase<DoctorToPatientDto, DoctorToPatient, DoctorToPatientFilterDto, IQuery<DoctorToPatient>>
    {
        public DoctorToPatientQueryObject(IMapper mapper, IQuery<DoctorToPatient> query) : base(mapper, query) { }

        protected override IQuery<DoctorToPatient> ApplyWhereClause(IQuery<DoctorToPatient> query, DoctorToPatientFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterByDoctorId(filter), definedPredicates);
            AddIfDefined(FilterByPatientId(filter), definedPredicates);
            if (definedPredicates.Count == 0) return query;
            if (definedPredicates.Count == 1) return query.Where(definedPredicates.First());
            var wherePredicate = new CompositePredicate(definedPredicates);
            return query.Where(wherePredicate);
        }

        private IPredicate FilterByDoctorId(DoctorToPatientFilterDto filter)
        {
            if (filter.DoctorId == Guid.Empty) return null;
            return new SimplePredicate(nameof(DoctorToPatient.DoctorId), ValueComparingOperator.Equal, filter.DoctorId);
        }

        private IPredicate FilterByPatientId(DoctorToPatientFilterDto filter)
        {
            if (filter.PatientId == Guid.Empty) return null;
            return new SimplePredicate(nameof(DoctorToPatient.PatientId), ValueComparingOperator.Equal, filter.PatientId);
        }

        private static void AddIfDefined(IPredicate doctorToPatientPredicate, ICollection<IPredicate> definedPredicates)
        {
            if (doctorToPatientPredicate != null)
            {
                definedPredicates.Add(doctorToPatientPredicate);
            }
        }
    }
}
