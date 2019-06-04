using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.QueryObjects;
using HospitalIS.BusinessLayer.Tests.QueryObjectTests.Common;
using HospitalIS.Infrastructure.Query.Predicates;
using HospitalIS.Infrastructure.Query.Predicates.Operators;
using HospitalISDBContext.Entities;
using HospitalISDBContext.Enums;
using NUnit.Framework;

namespace HospitalIS.BusinessLayer.Tests.QueryObjectTests
{
    [TestFixture]
    public class PatientQueryObjectTest
    {
        [Test]
        public async Task ApplyWhereClause_SimpleFilterWithName_ReturnsCorrectCompositePredicate()
        {
            var mockManager = new QueryMockManager();
            var predicateList = new List<IPredicate> {
                new SimplePredicate(nameof(Patient.Name), ValueComparingOperator.Equal, "Harry"),
                new SimplePredicate(nameof(Patient.Surname), ValueComparingOperator.Equal, "Maybourne")
            };
            var expectedPredicate = new CompositePredicate(predicateList);

            var mapperMock = mockManager.ConfigureMapperMock<Patient, PatientDto, PatientFilterDto>();
            var queryMock = mockManager.ConfigureQueryMock<Patient>();
            var patientQueryObject = new PatientQueryObject(mapperMock.Object, queryMock.Object);

            var unused = await patientQueryObject.ExecuteQuery(new PatientFilterDto { FullName = "Harry Maybourne" });

            Assert.AreEqual(mockManager.CapturedPredicate, expectedPredicate);
        }

        [Test]
        public async Task ApplyWhereClause_SimpleFilterWithIdentificationNumber_ReturnsCorrectSimplePredicate()
        {
            var mockManager = new QueryMockManager();
            var expectedPredicate = new SimplePredicate(nameof(Patient.IdentificationNumber), ValueComparingOperator.Equal, "999999");

            var mapperMock = mockManager.ConfigureMapperMock<Patient, PatientDto, PatientFilterDto>();
            var queryMock = mockManager.ConfigureQueryMock<Patient>();
            var patientQueryObject = new PatientQueryObject(mapperMock.Object, queryMock.Object);

            var unused = await patientQueryObject.ExecuteQuery(new PatientFilterDto { IdentificationNumber = "999999"});

            Assert.AreEqual(mockManager.CapturedPredicate, expectedPredicate);
        }
    }
}
