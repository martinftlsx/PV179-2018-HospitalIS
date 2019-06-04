using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using HospitalIS.Infrastructure;
using HospitalIS.Infrastructure.Query;
using HospitalIS.Infrastructure.UnitOfWork_;
using HospitalISInfrastructureEntityFramework;
using HospitalISInfrastructureEntityFramework.UnitOfWork;

namespace HospitalISDBContext.Config
{
    public class EntityFrameworkInstaller : IWindsorInstaller
    {
        internal const string ConnectionString = "Data source=(localdb)\\mssqllocaldb;Database=Lab08DemoEshopDatabaseSample;Trusted_Connection=True;MultipleActiveResultSets=true";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(() => new HospitalISDbContext())
                    .LifestyleTransient(),
                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<EntityFrameworkUnitOfWorkProvider>()
                    .LifestyleSingleton(),
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(EntityFrameworkRepository<>))
                    .LifestyleTransient(),
                Component.For(typeof(IQuery<>))
                    .ImplementedBy(typeof(EntityFrameworkQuery<>))
                    .LifestyleTransient()
            );
        }
    }
}
