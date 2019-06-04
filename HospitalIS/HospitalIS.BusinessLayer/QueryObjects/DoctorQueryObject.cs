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
using HospitalISDBContext.Enums;

namespace HospitalIS.BusinessLayer.QueryObjects
{
    public class DoctorQueryObject : QueryObjectBase<DoctorDto, Doctor, DoctorFilterDto, IQuery<Doctor>>
    {
        public DoctorQueryObject(IMapper mapper, IQuery<Doctor> query) : base(mapper, query) { }

        protected override IQuery<Doctor> ApplyWhereClause(IQuery<Doctor> query, DoctorFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();
            AddIfDefined(FilterByUsername(filter), definedPredicates);
            AddIfDefined(FilterByName(filter), definedPredicates);
            AddIfDefined(FilterBySpecialization(filter), definedPredicates);
            if (definedPredicates.Count == 0) return query;
            if (definedPredicates.Count == 1) return query.Where(definedPredicates.First());
            var wherePredicate = new CompositePredicate(definedPredicates);
            return query.Where(wherePredicate);
        }

        private IPredicate FilterByUsername(DoctorFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Username)) return null;
            return new SimplePredicate(nameof(Doctor.Username), ValueComparingOperator.Equal, filter.Username);
        }

        private IPredicate FilterBySpecialization(DoctorFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Specialization)) return null;
            var specialization = Enum.Parse(typeof(Specialization), filter.Specialization, true);
            return new SimplePredicate(nameof(Doctor.Specialization), ValueComparingOperator.Equal, specialization);
        }

        private IPredicate FilterByName(DoctorFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.FullName)) return null;
            char[] delim = { ' ' };
            var trimmedName = filter.FullName.Trim();
            string[] name = trimmedName.Split(delim, 2);
            if (name.Length != 2) return null;
            var predicates = new List<IPredicate>
            {
                new SimplePredicate(nameof(Doctor.Name), ValueComparingOperator.Equal, name[0]),
                new SimplePredicate(nameof(Doctor.Surname), ValueComparingOperator.Equal, name[1])
            };
            return new CompositePredicate(predicates);
        }

        private static void AddIfDefined(IPredicate doctorPredicate, ICollection<IPredicate> definedPredicates)
        {
            if (doctorPredicate != null)
            {
                definedPredicates.Add(doctorPredicate);
            }
        }
    }
}
