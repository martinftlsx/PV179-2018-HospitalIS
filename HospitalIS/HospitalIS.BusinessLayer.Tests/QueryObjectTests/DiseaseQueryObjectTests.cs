using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.QueryObjects;
using HospitalIS.BusinessLayer.Tests.QueryObjectTests.Common;
using HospitalIS.Infrastructure.Query;
using HospitalIS.Infrastructure.Query.Predicates;
using HospitalIS.Infrastructure.Query.Predicates.Operators;
using HospitalISDBContext.Entities;
using NUnit.Framework;

namespace HospitalIS.BusinessLayer.Tests.QueryObjectTests
{
    [TestFixture]
    public class DiseaseQueryObjectTests
    {
        [Test]
        public async Task ApplyWhereClause_SimpleFilterWithName_ReturnsCorrectSimplePredicate()
        {
            var mockManager = new QueryMockManager();
            var expectedPredicate = new SimplePredicate(nameof(Disease.Name), ValueComparingOperator.StringContains, "Death");

            var mapperMock = mockManager.ConfigureMapperMock<Disease, DiseaseDto, DiseaseFilterDto>();
            var queryMock = mockManager.ConfigureQueryMock<Disease>();
            var diseaseQueryObject = new DiseaseQueryObject(mapperMock.Object, queryMock.Object);

            var unused = await diseaseQueryObject.ExecuteQuery(new DiseaseFilterDto{DiseaseName = "Death"});

            Assert.AreEqual(mockManager.CapturedPredicate, expectedPredicate);
        }
        /*
        [Test]
        public async Task ApplyWhereClause_SimpleFilterWithSympthoms_ReturnsCorrectCompositePredicate()
        {
            var mockManager = new QueryMockManager();
            var predicateList = new List<IPredicate>
            {
                new SimplePredicate(nameof(Disease.Sympthoms), ValueComparingOperator.ContainsSympthom, "Immobilized"),
                new SimplePredicate(nameof(Disease.Sympthoms), ValueComparingOperator.ContainsSympthom, "Dead")
            };
            var expectedPredicate = new CompositePredicate(predicateList);

            var mapperMock = mockManager.ConfigureMapperMock<Disease, DiseaseDto, DiseaseFilterDto>();
            var queryMock = mockManager.ConfigureQueryMock<Disease>();
            var diseaseQueryObject = new DiseaseQueryObject(mapperMock.Object, queryMock.Object);

            var unused = await diseaseQueryObject.ExecuteQuery(new DiseaseFilterDto { NamesOfSympthoms = new []{"Immobilized", "Dead"}});

            Assert.AreEqual(mockManager.CapturedPredicate, expectedPredicate);
        }*/
    }
}
