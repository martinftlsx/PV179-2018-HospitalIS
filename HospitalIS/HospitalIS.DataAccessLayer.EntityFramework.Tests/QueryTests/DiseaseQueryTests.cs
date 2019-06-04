using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalIS.Infrastructure.Query;
using HospitalIS.Infrastructure.Query.Predicates;
using HospitalIS.Infrastructure.Query.Predicates.Operators;
using HospitalIS.Infrastructure.UnitOfWork_;
using HospitalISDBContext.Entities;
using NUnit.Framework;

namespace HospitalIS.DataAccessLayer.EntityFramework.Tests.QueryTests
{
    [TestFixture]
    public class DiseaseQueryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

        [Test]
        public async Task AppleyWhereClause_FilterByDisesaeName_ReturnsCorrectCount()
        {
            var diseaseQuery = Initializer.Container.Resolve<IQuery<Disease>>();
            var predicate = new SimplePredicate(nameof(Disease.Name), ValueComparingOperator.Equal, "Death");
            QueryResult<Disease> actualQueryResult;
            using (unitOfWorkProvider.Create())
            {
                actualQueryResult = await diseaseQuery.Where(predicate).ExecuteAsync();
            }
            Assert.IsTrue(actualQueryResult.Items.Count == 1);
        }
    }
}
