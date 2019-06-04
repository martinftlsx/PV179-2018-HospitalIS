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
    public class PatientQueryObject : QueryObjectBase<PatientDto, Patient, PatientFilterDto, IQuery<Patient>>
    {
        public PatientQueryObject(IMapper mapper, IQuery<Patient> query) : base(mapper, query) { }

        protected override IQuery<Patient> ApplyWhereClause(IQuery<Patient> query, PatientFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterByUsername(filter), definedPredicates);
            AddIfDefined(FilterByName(filter), definedPredicates);
            AddIfDefined(FilterByIdentificationNumber(filter), definedPredicates);
            if (definedPredicates.Count == 0) return query;
            if (definedPredicates.Count == 1) return query.Where(definedPredicates.First());
            var wherePredicate = new CompositePredicate(definedPredicates);
            return query.Where(wherePredicate);
        }

        private IPredicate FilterByUsername(PatientFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Username)) return null;
            return new SimplePredicate(nameof(Patient.Username), ValueComparingOperator.Equal, filter.Username);
        }

        private IPredicate FilterByName(PatientFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.FullName)) return null;
            char[] delim = { ' ' };
            var trimmedName = filter.FullName.Trim();
;           string[] name = filter.FullName.Split(delim, 2);
            if (name.Length != 2) return null;
            var predicates = new List<IPredicate>
            {
                new SimplePredicate(nameof(Patient.Name), ValueComparingOperator.Equal, name[0]),
                new SimplePredicate(nameof(Patient.Surname), ValueComparingOperator.Equal, name[1])
            };
            return new CompositePredicate(predicates);
        }

        private IPredicate FilterByIdentificationNumber(PatientFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.IdentificationNumber)) return null;
            return new SimplePredicate(nameof(Patient.IdentificationNumber), ValueComparingOperator.Equal, filter.IdentificationNumber);
        }

        private static void AddIfDefined(IPredicate patientPredicate, ICollection<IPredicate> definedPredicates)
        {
            if (patientPredicate != null)
            {
                definedPredicates.Add(patientPredicate);
            }
        }
    }
}
