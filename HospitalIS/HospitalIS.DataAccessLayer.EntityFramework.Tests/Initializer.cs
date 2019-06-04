using System.Data.Entity;
using Castle.Windsor;
using HospitalIS.DataAccessLayer.EntityFramework.Tests.Config;
using HospitalISDBContext;
using NUnit.Framework;

namespace HospitalIS.DataAccessLayer.EntityFramework.Tests
{
    [SetUpFixture]
    public class Initializer
    {
        internal static readonly IWindsorContainer Container = new WindsorContainer();
    
    /// <summary>
    /// Initializes all tests
    /// </summary>
        [OneTimeSetUp]
        public void InitializeBusinessLayerTests()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
            Database.SetInitializer(new DropCreateDatabaseAlways<HospitalISDbContext>());
            Container.Install(new EntityFrameworkTestInstaller());
        }
    }
}
