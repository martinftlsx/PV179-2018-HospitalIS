using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.Facades;
using HospitalIS.BusinessLayer.QueryObjects.Common;
using HospitalIS.BusinessLayer.Services.Sympthoms;
using HospitalIS.BusinessLayer.Services.Diseases;
using HospitalIS.BusinessLayer.Tests.FacadesTests.Common;
using HospitalIS.Infrastructure;
using HospitalIS.Infrastructure.Query;
using HospitalISDBContext.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.Tests.FacadesTests
{
    [TestFixture]
    public class DiseaseFacadeTests
    {
        [Test]
        public async Task GetDiseaseByNameAsync()
        {
            var diseaseId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            const string name = "tuberkulosis";

            var diseaseDto = new DiseaseDto
            {
                Id = diseaseId,
                Name = name
            };

            var expectedResult = new List<DiseaseDto> { diseaseDto };

            var returnedResult = new QueryResultDto<DiseaseDto, DiseaseFilterDto>
            {
                Items = new List<DiseaseDto> { diseaseDto }
            };

            var mockManager = new FacadeMockManager();
            var diseaseRepositoryMock = mockManager.ConfigureRepositoryMock<Disease>();
            var diseaseQueryMock = mockManager.ConfigureQueryObjectMock<DiseaseDto, Disease, DiseaseFilterDto>(returnedResult);
            var sympthomRepositoryMock = mockManager.ConfigureRepositoryMock<Sympthom>();
            var sympthomQueryMock = mockManager.ConfigureQueryObjectMock<SympthomDto, Sympthom, SympthomFilterDto>(null);
            var diseaseFacade = CreateDiseaseFacade(diseaseQueryMock, diseaseRepositoryMock, sympthomQueryMock, sympthomRepositoryMock);

            var actualResult = await diseaseFacade.GetDiseaseByNameAsync("tuber");

            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public async Task CreateDiseaseAsync()
        {
            var diseaseId = Guid.Parse("00000000-0000-0000-0000-000000000002");
            const string name = "tuberkulosis";

            var returnedResult = new QueryResultDto<DiseaseDto, DiseaseFilterDto>
            {
                Items = new List<DiseaseDto> { new DiseaseDto { Id = diseaseId } }
            };

            var mockManager = new FacadeMockManager();
            var diseaseRepositoryMock = mockManager.ConfigureCreateRepositoryMock<Disease>(nameof(Disease.Id));
            var diseaseQueryMock = mockManager.ConfigureQueryObjectMock<DiseaseDto, Disease, DiseaseFilterDto>(returnedResult);
            var sympthomRepositoryMock = mockManager.ConfigureRepositoryMock<Sympthom>();
            var sympthomQueryMock = mockManager.ConfigureQueryObjectMock<SympthomDto, Sympthom, SympthomFilterDto>(null);
            var diseaseFacade = CreateDiseaseFacade(diseaseQueryMock, diseaseRepositoryMock, sympthomQueryMock, sympthomRepositoryMock);

            await diseaseFacade.CreateDiseaseAsync(new DiseaseDto { Id = diseaseId, Name = name } );

            Assert.AreEqual(diseaseId, mockManager.CapturedCreatedId);
        }

        private static DiseaseFacade CreateDiseaseFacade(Mock<QueryObjectBase<DiseaseDto, Disease, DiseaseFilterDto, IQuery<Disease>>> diseaseQueryMock, Mock<IRepository<Disease>> diseaseRepository,
        Mock<QueryObjectBase<SympthomDto, Sympthom, SympthomFilterDto, IQuery<Sympthom>>> sympthomQueryMock, Mock<IRepository<Sympthom>> sympthomRepository)
        {
            var uowMock = FacadeMockManager.ConfigureUowMock();
            var mapperMock = FacadeMockManager.ConfigureRealMapper();
            var diseaseService = new DiseaseService(mapperMock, diseaseRepository.Object, diseaseQueryMock.Object);
            var healthCardService = new SympthomService(mapperMock, sympthomRepository.Object, sympthomQueryMock.Object);

            var diseaseFacade = new DiseaseFacade(uowMock.Object, diseaseService, healthCardService);
            return diseaseFacade;
        }
    }
}
