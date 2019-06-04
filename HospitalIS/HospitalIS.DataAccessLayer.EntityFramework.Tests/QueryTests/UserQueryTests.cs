using System;
using HospitalIS.Infrastructure.Query;
using HospitalIS.Infrastructure.Query.Predicates;
using HospitalIS.Infrastructure.Query.Predicates.Operators;
using HospitalIS.Infrastructure.UnitOfWork_;
using HospitalISDBContext.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalIS.DataAccessLayer.EntityFramework.Tests.QueryTests
{
    [TestFixture]
    public class UserQueryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

        [Test]
        public async Task ExecuteAsync_SimpleWherePredicate_ReturnsCorrectQueryResult()
        {
            QueryResult<Doctor> actualQueryResult;
            var doctorQuery = Initializer.Container.Resolve<IQuery<Doctor>>();
            var expectedQueryResult = new QueryResult<Doctor>(new List<Doctor>{
                new Doctor
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Username = "John",
                    PasswordHash = "123",
                    PasswordSalt = "12",
                    Name = "John",
                    Surname = "Doe",
                    Specialization = HospitalISDBContext.Enums.Specialization.AllergistImmunologist
                }
                }, 1);
            var predicate = new SimplePredicate(nameof(Doctor.Name), ValueComparingOperator.Equal, "John");
            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await doctorQuery.Where(predicate).ExecuteAsync();
            }
            Assert.AreEqual(actualQueryResult, expectedQueryResult);
        }

        [Test]
        public async Task ExecuteAsync_ComplexWherePredicate_ReturnsCorretQueryResult()
        {
            QueryResult<Doctor> actualQueryResult;
            var doctorQuery = Initializer.Container.Resolve<IQuery<Doctor>>();
            var expectedQueryResult = new QueryResult<Doctor>(new List<Doctor>
            {
                new Doctor
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Username = "John",
                    PasswordHash = "123",
                    PasswordSalt = "12",
                    Name = "John",
                    Surname = "Doe",
                    Specialization = HospitalISDBContext.Enums.Specialization.AllergistImmunologist
                },
                new Doctor
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Username = "Mac",
                    PasswordHash = "123",
                    PasswordSalt = "12",
                    Name = "Mac",
                    Surname = "Kane",
                    Specialization = HospitalISDBContext.Enums.Specialization.Ophthalmologist
                }
            }, 2);
            var predicate = new CompositePredicate(new List<IPredicate>
            {
                new SimplePredicate(nameof(Doctor.Name), ValueComparingOperator.Equal, "John"),
                new SimplePredicate(nameof(Doctor.Name), ValueComparingOperator.Equal, "Mac")
            }, LogicalOperator.OR);
            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await doctorQuery.Where(predicate).ExecuteAsync();
            }
            Assert.AreEqual(actualQueryResult, expectedQueryResult);
        }

        [Test]
        public async Task ExecuteAsync_OrdersDoctorsByName_ReturnsCorretQueryResult()
        {
            QueryResult<Doctor> actualQueryResult;
            var doctorQuery = Initializer.Container.Resolve<IQuery<Doctor>>();
            var expectedQueryResult = new QueryResult<Doctor>(new List<Doctor>
            {
                new Doctor
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Username = "John",
                    PasswordHash = "123",
                    PasswordSalt = "12",
                    Name = "John",
                    Surname = "Doe",
                    Specialization = HospitalISDBContext.Enums.Specialization.AllergistImmunologist
                },
                new Doctor
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Username = "Mac",
                    PasswordHash = "123",
                    PasswordSalt = "12",
                    Name = "Mac",
                    Surname = "Kane",
                    Specialization = HospitalISDBContext.Enums.Specialization.Ophthalmologist
                }
            }, 2);
            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await doctorQuery.SortBy(nameof(Doctor.Name), false).ExecuteAsync();
            }
            Assert.IsTrue(actualQueryResult.Items[0].Name.Equals("Mac") && actualQueryResult.Items[1].Name.Equals("John"));
        }

        [Test]
        public async Task ExecuteAsync_RetrieveSecondCategoriesPage_ReturnsCorrectPage()
        {
            const int pageSize = 1;
            const int requestedPage = 2;
            QueryResult<Doctor> actualQueryResult;
            var doctorQuery = Initializer.Container.Resolve<IQuery<Doctor>>();
            var expectedQueryResult = new QueryResult<Doctor>(new List<Doctor>
            {
                new Doctor
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Username = "Mac",
                    PasswordHash = "123",
                    PasswordSalt = "12",
                    Name = "Mac",
                    Surname = "Kane",
                    Specialization = HospitalISDBContext.Enums.Specialization.Ophthalmologist
                }
            }, 2, pageSize, requestedPage);

            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await doctorQuery.Page(requestedPage, pageSize).ExecuteAsync();
            }

            Assert.AreEqual(actualQueryResult, expectedQueryResult);
        }
    }
}
