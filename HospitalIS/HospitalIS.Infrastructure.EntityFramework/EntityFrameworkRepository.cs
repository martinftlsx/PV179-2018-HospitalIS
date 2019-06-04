using System;
using HospitalIS.Infrastructure;
using HospitalIS.Infrastructure.UnitOfWork_;
using HospitalISInfrastructureEntityFramework.UnitOfWork;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HospitalISInfrastructureEntityFramework
{
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IUnitOfWorkProvider provider;

        protected DbContext Context => ((EntityFrameworkUnitOfWork)provider.GetUnitOfWorkInstance()).Context;

        public EntityFrameworkRepository(IUnitOfWorkProvider provider)
        {
            this.provider = provider;
        }

        public Guid Create(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            Context.Set<TEntity>().Add(entity);
            return entity.Id;
        }

        public Guid CreateWithoutId (TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return entity.Id;
        }

        public void Delete(Guid id)
        {
            var entity = Context.Set<TEntity>().Find(id);
            if (entity != null) {
                Context.Set<TEntity>().Remove(entity);
            }
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(Guid id, params string[] includes)
        {
            DbQuery<TEntity> context = Context.Set<TEntity>();
            foreach(var include in includes)
            {
                context.Include(include);
            }
            return await context.SingleOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public void Update(TEntity entity)
        {
            var entityToUpdate = Context.Set<TEntity>().Find(entity.Id);
            Context.Entry(entityToUpdate).CurrentValues.SetValues(entity); 
        }
    }
}
