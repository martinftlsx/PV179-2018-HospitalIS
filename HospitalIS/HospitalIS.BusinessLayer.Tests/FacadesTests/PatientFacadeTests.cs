using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.BusinessLayer.Facades;
using HospitalIS.BusinessLayer.QueryObjects.Common;
using HospitalIS.BusinessLayer.Services.HealthCards;
using HospitalIS.BusinessLayer.Services.Patients;
using HospitalIS.BusinessLayer.Tests.FacadesTests.Common;
using HospitalIS.Infrastructure;
using HospitalIS.Infrastructure.Query;
using HospitalISDBContext.Entities;
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
    public class PatientFacadeTests
    {
        [Test]
        public async Task GetPatientAsync()
        {
            var patientId = Guid.Parse("00000000-0000-0000-0000-000000000100");
            const string identificationNumber = "abcd1234";
            const string name = "Pete";
            const string surname = "Melark";
            const string email = "hungergames@gmail.com";

            var expectedPatientDto = new PatientDto
            {
                Id = patientId,
                IdentificationNumber = identificationNumber,
                Name = name,
                Surname = surname,
                Email = email,
            };
            var expectedPacient = new Patient
            {
                Id = patientId,
                IdentificationNumber = identificationNumber,
                Name = name,
                Surname = surname,
                Email = email
            };
            var mockManager = new FacadeMockManager();
            var patientRepositoryMock = mockManager.ConfigureGetRepositoryMock(expectedPacient);
            var patientQueryMock = mockManager.ConfigureQueryObjectMock<PatientDto, Patient, PatientFilterDto>(null);
            var healthCardRepositoryMock = mockManager.ConfigureRepositoryMock<HealthCard>();
            var healthCardMock = mockManager.ConfigureQueryObjectMock<HealthCardDto, HealthCard, HealthCardFilterDto>(null);
            var patientFacade = CreatePatientFacade(patientQueryMock, patientRepositoryMock, healthCardMock, healthCardRepositoryMock);

            var actualPatientDto = await patientFacade.GetPatientAsync(patientId);

            Assert.AreEqual(actualPatientDto.Name, expectedPatientDto.Name);
            Assert.AreEqual(actualPatientDto.Surname, expectedPatientDto.Surname);
            Assert.AreEqual(actualPatientDto.Email, expectedPatientDto.Email);
            Assert.AreEqual(actualPatientDto.IdentificationNumber, actualPatientDto.IdentificationNumber);
            Assert.AreEqual(actualPatientDto.Id, expectedPatientDto.Id);
            //Assert.AreEqual(actualPatientDto, expectedPatientDto);
        }

        [Test]
        public async Task GetDoctorsForPatientAsync()
        {
            var patientId = Guid.Parse("00000000-0000-0000-0000-000000000100");
            const string identificationNumber = "abcd1234";
            const string name = "Pete";
            const string surname = "Melark";
            const string email = "hungergames@gmail.com";

            var mapperMock = FacadeMockManager.ConfigureRealMapper();

            var doctorDto = new DoctorDto
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                Name = "John",
                Surname = "Ferenvaros",
                Specialization = HospitalISDBContext.Enums.Specialization.Neurologist
            };

            var expectedResult = new List<DoctorDto> { doctorDto };
            var expectedPacient = new Patient
            {
                Id = patientId,
                IdentificationNumber = identificationNumber,
                Name = name,
                Surname = surname,
                Email = email,
                Doctors = new List<Doctor>
                {
                    mapperMock.Map<Doctor>(doctorDto)
                }
            };

            var mockManager = new FacadeMockManager();
            var patientRepositoryMock = mockManager.ConfigureGetRepositoryMock(expectedPacient);
            var patientQueryMock = mockManager.ConfigureQueryObjectMock<PatientDto, Patient, PatientFilterDto>(null);
            var healthCardRepositoryMock = mockManager.ConfigureRepositoryMock<HealthCard>();
            var healthCardMock = mockManager.ConfigureQueryObjectMock<HealthCardDto, HealthCard, HealthCardFilterDto>(null);
            var patientFacade = CreatePatientFacade(patientQueryMock, patientRepositoryMock, healthCardMock, healthCardRepositoryMock);

            var actualResult = await patientFacade.GetDoctorsForPatientAsync(patientId);
            Assert.AreEqual(expectedResult, actualResult);
        }

        private static PatientFacade CreatePatientFacade(Mock<QueryObjectBase<PatientDto, Patient, PatientFilterDto, IQuery<Patient>>> patientQueryMock, Mock<IRepository<Patient>> patientRepository,
        Mock<QueryObjectBase<HealthCardDto, HealthCard, HealthCardFilterDto, IQuery<HealthCard>>> healthCardQueryMock, Mock<IRepository<HealthCard>> healthCardRepository)
        {
            var uowMock = FacadeMockManager.ConfigureUowMock();
            var mapperMock = FacadeMockManager.ConfigureRealMapper();
            var patientService = new PatientService(mapperMock, patientRepository.Object, patientQueryMock.Object);
            var healthCardService = new HealthCardService(mapperMock, healthCardRepository.Object, healthCardQueryMock.Object);

            var patientFacade = new PatientFacade(uowMock.Object, patientService, healthCardService);
            return patientFacade;
        }
    }
}
