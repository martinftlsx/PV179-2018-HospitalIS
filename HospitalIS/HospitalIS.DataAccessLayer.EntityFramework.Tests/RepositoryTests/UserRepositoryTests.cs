using HospitalIS.Infrastructure;
using HospitalIS.Infrastructure.UnitOfWork_;
using HospitalISDBContext.Entities;
using HospitalISInfrastructureEntityFramework;
using HospitalISInfrastructureEntityFramework.UnitOfWork;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalIS.DataAccessLayer.EntityFramework.Tests.RepositoryTests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider = Initializer.Container.Resolve<IUnitOfWorkProvider>();

        private readonly IRepository<Doctor> doctorRepository = Initializer.Container.Resolve<IRepository<Doctor>>();

        private readonly Guid doctorJohnDoeId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        private readonly Guid doctorMacKaneId = Guid.Parse("00000000-0000-0000-0000-000000000002");
        private readonly Guid patientHarryMaybourneId = Guid.Parse("00000000-0000-0000-0000-000000000003");

        [Test]
        public async Task GetDoctorAcsync_InDB_NoIncludes_ReturnsCorrectDoctor()
        {
            Doctor doctorJohnDoe;
            using (unitOfWorkProvider.Create())
            {
                doctorJohnDoe = await doctorRepository.GetAsync(doctorJohnDoeId);
            }

            Assert.AreEqual(doctorJohnDoe.Id, doctorJohnDoeId);
        }

        [Test]
        public async Task GetDoctorAsync_InDB_WithIncludes_ReturnsCorrectDoctor()
        {
            Doctor doctorMacKane;
            using (unitOfWorkProvider.Create())
            {
                doctorMacKane = await doctorRepository.GetAsync(doctorMacKaneId, nameof(Doctor.Patients));
            }
            Console.WriteLine(doctorMacKane != null);
            Assert.IsTrue(doctorMacKane.Id.Equals(doctorMacKaneId) && doctorMacKane.Patients.First().Id == patientHarryMaybourneId);
        }

        [Test]
        public async Task CreateDoctorAsync_NotInDB_CreateNewDoctor()
        {
            var doctorSavosAren = new Doctor
            {
                Username = "Savos",
                PasswordHash = "123",
                PasswordSalt = "12",
                Name = "Savos",
                Surname = "Aren",
                AccessRights = HospitalISDBContext.Enums.AccessRights.Doctor
            };
            using (var uow = unitOfWorkProvider.Create())
            {
                doctorRepository.Create(doctorSavosAren);
                await uow.Commit();
            }
            Assert.IsTrue(doctorSavosAren.Id != null);
        }

        [Test]
        public async Task UpdateDoctorAsync_DoctorSelected_UpdateDatabase()
        {
            Doctor updatedDoctor;
            var newDoctor = new Doctor
            {
                Id = doctorMacKaneId,
                Username = "Savos",
                PasswordHash = "123",
                PasswordSalt = "12",
                Name = "Savos",
                Surname = "Aren",
                AccessRights = HospitalISDBContext.Enums.AccessRights.Doctor,
            };

            using(var uow = unitOfWorkProvider.Create())
            {
                doctorRepository.Update(newDoctor);
                await uow.Commit();
                updatedDoctor = await doctorRepository.GetAsync(doctorMacKaneId);
            }

            Assert.IsTrue(newDoctor.Name.Equals(updatedDoctor.Name));
        }

        [Test]
        public async Task DeleteDoctorAsync_InDB_DeletesDoctor()
        {
            Doctor deletedDoctor;

            using (var uow = unitOfWorkProvider.Create())
            {
                doctorRepository.Delete(doctorMacKaneId);
                await uow.Commit();
                deletedDoctor = await doctorRepository.GetAsync(doctorMacKaneId);
            }
            Console.WriteLine(deletedDoctor?.Name);
            Assert.IsTrue(deletedDoctor == null);
        }
    }
}
