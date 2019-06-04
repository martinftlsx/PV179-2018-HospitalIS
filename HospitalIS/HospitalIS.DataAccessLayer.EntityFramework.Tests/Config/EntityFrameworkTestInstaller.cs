using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using HospitalIS.Infrastructure;
using HospitalIS.Infrastructure.Query;
using HospitalIS.Infrastructure.UnitOfWork_;
using HospitalISDBContext;
using HospitalISDBContext.Entities;
using HospitalISInfrastructureEntityFramework;
using HospitalISInfrastructureEntityFramework.UnitOfWork;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace HospitalIS.DataAccessLayer.EntityFramework.Tests.Config
{
    public class EntityFrameworkTestInstaller : IWindsorInstaller
    {
        private const string TestDbConnectionString = "InMemoryTestDBHospitalIS";

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Func<DbContext>>()
                    .Instance(InitializeDatabase)
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

        private DbContext InitializeDatabase()
        {
            var context = new HospitalISDbContext(Effort.DbConnectionFactory.CreatePersistent(TestDbConnectionString));
            //context.Users.RemoveRange(context.Users);
            //context.SaveChanges();

            var doctorJohnDoe = new Doctor
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Username = "John",
                PasswordHash = "123",
                PasswordSalt = "12",
                Name = "John",
                Surname = "Doe",
                Specialization = HospitalISDBContext.Enums.Specialization.AllergistImmunologist
            };
            var doctorMacKane = new Doctor
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Username = "Mac",
                PasswordHash = "123",
                PasswordSalt = "12",
                Name = "Mac",
                Surname = "Kane",
                Specialization = HospitalISDBContext.Enums.Specialization.Ophthalmologist
            };

            var patientHarryMaybourne = new Patient
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Username = "Harry",
                PasswordHash = "123",
                PasswordSalt = "12",
                Name = "Harry",
                Surname = "Maybourne",
                DateOfBirth = new DateTime(1967, 1, 5),
                IdentificationNumber = "670105"
            };
            var patientRickUnseen = new Patient
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                Username = "Rick",
                PasswordHash = "123",
                PasswordSalt = "12",
                Name = "Rick",
                Surname = "Unseen",
                DateOfBirth = new DateTime(1985, 8, 9),
                IdentificationNumber = "850809"
            };

            var death = new Disease
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Name = "Death"
            };
            var deathSympthom1 = new Sympthom
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Name = "Immobilized"
            };
            var deathSympthom2 = new Sympthom
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Name = "Asleep"
            };
            var deathSympthom3 = new Sympthom
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Name = "Dead"
            };
            //death.Sympthoms.Add(deathSympthom1);
            //death.Sympthoms.Add(deathSympthom2);
            //death.Sympthoms.Add(deathSympthom3);

            doctorJohnDoe.Patients.Add(patientHarryMaybourne);
            doctorJohnDoe.Patients.Add(patientRickUnseen);
            doctorMacKane.Patients.Add(patientHarryMaybourne);
            patientHarryMaybourne.Doctors.Add(doctorJohnDoe);
            patientHarryMaybourne.Doctors.Add(doctorMacKane);
            patientRickUnseen.Doctors.Add(doctorJohnDoe);

            context.Users.AddOrUpdate(doctorJohnDoe);
            context.Users.AddOrUpdate(doctorMacKane);
            context.Users.AddOrUpdate(patientHarryMaybourne);
            context.Users.AddOrUpdate(patientRickUnseen);
            context.Diseases.AddOrUpdate(death);
            context.Sympthoms.AddOrUpdate(deathSympthom1);
            context.Sympthoms.AddOrUpdate(deathSympthom2);
            context.Sympthoms.AddOrUpdate(deathSympthom2);

            context.SaveChanges();

            return context;
        }
    }
}
