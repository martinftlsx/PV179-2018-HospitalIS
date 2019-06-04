using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.Facades;
using HospitalIS.BusinessLayer.QueryObjects.Common;
using HospitalIS.BusinessLayer.Services.Doctors;
using HospitalIS.BusinessLayer.Tests.FacadesTests.Common;
using HospitalIS.Infrastructure;
using HospitalIS.Infrastructure.Query;
using HospitalISDBContext.Entities;
using HospitalISDBContext.Enums;
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
    public class DoctorFacadeTests
    {
        [Test]
        public async Task GetDoctorSpecializationAsync()
        {
            var doctorId = Guid.Parse("00000000-0000-0000-0000-000000000011");
            const string name = "John";
            const string surname = "Malecki";
            const Specialization specialization = Specialization.Cardiologist;

            var doctorId2 = Guid.Parse("00000000-0000-0000-0000-000000000011");
            const string name2 = "Pavel";
            const string surname2 = "Loungry";
            const Specialization specialization2 = Specialization.Cardiologist;

            var doctorDto1 = new DoctorDto
            {
                Id = doctorId,
                Name = name,
                Surname = surname,
                Specialization = specialization
            };

            var doctorDto2 = new DoctorDto
            {
                Id = doctorId2,
                Name = name2,
                Surname = surname2,
                Specialization = specialization2
            };

            var expectedResult = new List<DoctorDto> { doctorDto1, doctorDto2 };

            var returnedResult = new QueryResultDto<DoctorDto, DoctorFilterDto>
            {
                Items = expectedResult
            };

            var mockManager = new FacadeMockManager();
            var doctorRepositoryMock = mockManager.ConfigureRepositoryMock<Doctor>();
            var doctorQueryMock = mockManager.ConfigureQueryObjectMock<DoctorDto, Doctor, DoctorFilterDto>(returnedResult);
            var doctorFacade = CreateDoctorFacade(doctorQueryMock, doctorRepositoryMock);

            var actualResult = await doctorFacade.GetDoctorBySpecializationAsync("Cardiologist");

            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public async Task EditDoctorAsync()
        {
            var doctorId = Guid.Parse("00000000-0000-0000-0000-000000000011");
            const string name = "John";
            const string surname = "Malecki";
            const Specialization specialization = Specialization.Anesthesiologist;

            var returnedDoctor = new Doctor
            {
                Id = doctorId,
                Name = name,
                Surname = surname,
                Specialization = specialization
            };

            var updatedDoctor = new DoctorDto
            {
                Id = doctorId,
                Name = name,
                Surname = surname,
                Specialization = Specialization.AllergistImmunologist
            };

            var mockManager = new FacadeMockManager();
            var doctorRepositoryMock = mockManager.ConfigureGetAndUpdateRepositoryMock<Doctor>(returnedDoctor, nameof(Doctor.Specialization));
            var doctorQueryMock = mockManager.ConfigureQueryObjectMock<DoctorDto, Doctor, DoctorFilterDto>(null);
            var doctorFacade = CreateDoctorFacade(doctorQueryMock, doctorRepositoryMock);

            await doctorFacade.EditDoctorAsync(updatedDoctor);

            Assert.AreEqual((int)updatedDoctor.Specialization, mockManager.CapturedUpdatedUnit);
        }

        private static DoctorFacade CreateDoctorFacade(Mock<QueryObjectBase<DoctorDto, Doctor, DoctorFilterDto, IQuery<Doctor>>> doctorQueryMock, Mock<IRepository<Doctor>> doctorRepository)
        {
            var uowMock = FacadeMockManager.ConfigureUowMock();
            var mapperMock = FacadeMockManager.ConfigureRealMapper();
            var doctorService = new DoctorService(mapperMock, doctorRepository.Object, doctorQueryMock.Object);
          
            var doctorFacade = new DoctorFacade(uowMock.Object, doctorService);
            return doctorFacade;
        }
    }
}
