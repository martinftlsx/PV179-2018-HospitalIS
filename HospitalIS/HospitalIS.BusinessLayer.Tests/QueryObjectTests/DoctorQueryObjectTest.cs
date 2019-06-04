using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.QueryObjects;
using HospitalIS.BusinessLayer.Tests.QueryObjectTests.Common;
using HospitalIS.Infrastructure.Query.Predicates;
using HospitalIS.Infrastructure.Query.Predicates.Operators;
using HospitalISDBContext.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalISDBContext.Enums;

namespace HospitalIS.BusinessLayer.Tests.QueryObjectTests
{
    [TestFixture]
    public class DoctorQueryObjectTest
    {
        [Test]
        public async Task ApplyWhereClause_SimpleFilterWithName_ReturnsCorrectCompositePredicate()
        {
            var mockManager = new QueryMockManager();
            var predicateList = new List<IPredicate> {
                new SimplePredicate(nameof(Doctor.Name), ValueComparingOperator.Equal, "John"),
                new SimplePredicate(nameof(Doctor.Surname), ValueComparingOperator.Equal, "Doe")
            };
            var expectedPredicate = new CompositePredicate(predicateList);

            var mapperMock = mockManager.ConfigureMapperMock<Doctor, DoctorDto, DoctorFilterDto>();
            var queryMock = mockManager.ConfigureQueryMock<Doctor>();
            var doctorQueryObject = new DoctorQueryObject(mapperMock.Object, queryMock.Object);

            var unused = await doctorQueryObject.ExecuteQuery(new DoctorFilterDto { FullName = "John Doe" });

            Assert.AreEqual(mockManager.CapturedPredicate, expectedPredicate);
        }

        [Test]
        public async Task ApplyWhereClause_SimpleFilterWithSpecialization_ReturnsCorrectSimplePredicate()
        {
            var mockManager = new QueryMockManager();
            var expectedPredicate = new SimplePredicate(nameof(Doctor.Specialization), ValueComparingOperator.Equal, Specialization.Ophthalmologist);

            var mapperMock = mockManager.ConfigureMapperMock<Doctor, DoctorDto, DoctorFilterDto>();
            var queryMock = mockManager.ConfigureQueryMock<Doctor>();
            var doctorQueryObject = new DoctorQueryObject(mapperMock.Object, queryMock.Object);

            var unused = await doctorQueryObject.ExecuteQuery(new DoctorFilterDto { Specialization = "Ophthalmologist"});

            Assert.AreEqual(mockManager.CapturedPredicate, expectedPredicate);
        }
    }
}
