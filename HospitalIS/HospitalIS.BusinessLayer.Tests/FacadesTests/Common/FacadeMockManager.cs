using AutoMapper;
using HospitalIS.BusinessLayer.Config;
using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using HospitalIS.BusinessLayer.QueryObjects.Common;
using HospitalIS.Infrastructure;
using HospitalIS.Infrastructure.Query;
using HospitalIS.Infrastructure.UnitOfWork_;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.Tests.FacadesTests.Common
{
    public class FacadeMockManager
    {
        internal Guid CapturedCreatedId { get; private set; }

        internal int CapturedUpdatedUnit { get; private set; }

        internal static IMapper ConfigureRealMapper() => new Mapper(new MapperConfiguration(MappingConfig.ConfigureMapping));

        internal static Mock<IUnitOfWorkProvider> ConfigureUowMock()
        {
            var uowMock = new Mock<IUnitOfWorkProvider>(MockBehavior.Loose);
            uowMock.Setup(uow => uow.Create()).Returns(new StubUow());
            return uowMock;
        }

        internal Mock<IRepository<TEntity>> ConfigureRepositoryMock<TEntity>() where TEntity : class, IEntity, new()
        {
            return new Mock<IRepository<TEntity>>(MockBehavior.Loose);
        }

        internal Mock<IRepository<TEntity>> ConfigureGetRepositoryMock<TEntity>(TEntity result) where TEntity : class, IEntity, new()
        {
            var mockRepository = new Mock<IRepository<TEntity>>(MockBehavior.Loose);
            mockRepository.Setup(repository => repository.GetAsync(It.IsAny<Guid>(), It.IsAny<string[]>())).ReturnsAsync(result);
            mockRepository.Setup(repository => repository.GetAsync(It.IsAny<Guid>())).ReturnsAsync(result);
            return mockRepository;
        }

        internal Mock<IRepository<TEntity>> ConfigureCreateRepositoryMock<TEntity>(string propertyName) where TEntity : class, IEntity, new()
        {
            var mockRepository = new Mock<IRepository<TEntity>>(MockBehavior.Loose);
            mockRepository.Setup(repo => repo.Create(It.IsAny<TEntity>()))
                .Callback<TEntity>(param => CapturedCreatedId = (Guid)(param.GetType().GetProperty(propertyName)?.GetValue(param) ?? Guid.Empty));
            return mockRepository;
        }

        internal Mock<IRepository<TEntity>> ConfigureGetAndUpdateRepositoryMock<TEntity>(TEntity result, string propertyName) where TEntity : class, IEntity, new()
        {
            var mockRepository = ConfigureGetRepositoryMock(result);
            mockRepository.Setup(repo => repo.Update(It.IsAny<TEntity>()))
                .Callback<TEntity>(param => CapturedUpdatedUnit = (int)(param.GetType().GetProperty(propertyName)?.GetValue(param) ?? 0));
            return mockRepository;
        }

        internal Mock<QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>>>
            ConfigureQueryObjectMock<TDto, TEntity, TFilterDto>(QueryResultDto<TDto, TFilterDto> result)
            where TEntity : class, IEntity, new()
            where TFilterDto : FilterDtoBase
        {
            var queryMock = new Mock<QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>>>(MockBehavior.Loose, null, null);
            queryMock
                .Setup(query => query.ExecuteQuery(It.IsAny<TFilterDto>()))
                .ReturnsAsync(result);
            return queryMock;
        }
    }
}
