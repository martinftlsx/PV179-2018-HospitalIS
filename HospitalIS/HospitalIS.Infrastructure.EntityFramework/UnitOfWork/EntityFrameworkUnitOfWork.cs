using HospitalIS.Infrastructure.UnitOfWork_;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace HospitalISInfrastructureEntityFramework.UnitOfWork
{
    public class EntityFrameworkUnitOfWork : UnitOfWorkBase
    {
        public DbContext Context { get; }

        public EntityFrameworkUnitOfWork(Func<DbContext> dbContextFactory)
        {
            Context = dbContextFactory?.Invoke() ?? throw new ArgumentException("dbContextFactory cannot be null");
            Context.Configuration.AutoDetectChangesEnabled = true;
        }

        protected override async Task CommitCore()
        {
            await Context.SaveChangesAsync();
        }

        public override void Dispose()
        {
            Context.Dispose();
        }
    }
}
