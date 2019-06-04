using HospitalISDBContext.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Security.Cryptography;

namespace HospitalISDBContext
{
    public class HospitalISInitializer : DropCreateDatabaseAlways<HospitalISDbContext>
    {
        private const int PBKDF2IterCount = 100000;
        private const int PBKDF2SubkeyLength = 160 / 8;
        private const int saltSize = 128 / 8;

        protected override void Seed(HospitalISDbContext context)
        {
            #region Doctors

            Doctor doctorJohnDoe;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloJohn", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                doctorJohnDoe = new Doctor
                {
                    Id = Guid.NewGuid(),
                    Username = "John",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "John",
                    Surname = "Doe",
                    Specialization = HospitalISDBContext.Enums.Specialization.Neurologist
                };
            }

            Doctor doctorMacKane;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloMac", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                doctorMacKane = new Doctor
                {
                    Id = Guid.NewGuid(),
                    Username = "Mac",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Mac",
                    Surname = "Kane",
                    Specialization = HospitalISDBContext.Enums.Specialization.Ophthalmologist
                };
            }

            Doctor doctorJimmyFella;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloJimmy", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                doctorJimmyFella = new Doctor
                {
                    Id = Guid.NewGuid(),
                    Username = "Jimmy",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Jimmy",
                    Surname = "Fella",
                    Specialization = HospitalISDBContext.Enums.Specialization.Cardiologist
                };
            }

            Doctor doctorPeterParker;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloPeter", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                doctorPeterParker = new Doctor
                {
                    Id = Guid.NewGuid(),
                    Username = "Peter",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Peter",
                    Surname = "Parker",
                    Specialization = HospitalISDBContext.Enums.Specialization.Dermatologist
                };
            }

            Doctor doctorMatildaBrown;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloMatilda", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                doctorMatildaBrown = new Doctor
                {
                    Id = Guid.NewGuid(),
                    Username = "Matilda",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Matilda",
                    Surname = "Brown",
                    Specialization = HospitalISDBContext.Enums.Specialization.Pediatrician
                };
            }

            Doctor doctorVictoriaTerace;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloVictoria", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                doctorVictoriaTerace = new Doctor
                {
                    Id = Guid.NewGuid(),
                    Username = "Victoria",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Victoria",
                    Surname = "Terace",
                    Specialization = HospitalISDBContext.Enums.Specialization.Psychiatrist
                };
            }

            Doctor doctorSamirRator;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloSamir", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                doctorSamirRator = new Doctor
                {
                    Id = Guid.NewGuid(),
                    Username = "Samir",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Samir",
                    Surname = "Rator",
                    Specialization = HospitalISDBContext.Enums.Specialization.Radiologist
                };
            }

            Doctor doctorOliverGernitzky;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloOliver", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);

                doctorOliverGernitzky = new Doctor
                {
                    Id = Guid.NewGuid(),
                    Username = "Oliver",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Oliver",
                    Surname = "Gernitzky",
                    Specialization = HospitalISDBContext.Enums.Specialization.Surgeon
                };
            }

            #endregion

            #region Patients

            Patient patientHarryMaybourne;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloHarry", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                patientHarryMaybourne = new Patient
                {
                    Id = Guid.NewGuid(),
                    Username = "Harry",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Harry",
                    Surname = "Maybourne",
                    DateOfBirth = new DateTime(1967, 1, 5),
                    IdentificationNumber = "670105"
                };
            }

            Patient patientRickUnseen;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloRick", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                patientRickUnseen = new Patient
                {
                    Id = Guid.NewGuid(),
                    Username = "Rick",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Rick",
                    Surname = "Unseen",
                    DateOfBirth = new DateTime(1985, 8, 9),
                    IdentificationNumber = "850809"
                };
            }

            Patient patientJulianRetina;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloJulian", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                patientJulianRetina = new Patient
                {
                    Id = Guid.NewGuid(),
                    Username = "Julian",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Julian",
                    Surname = "Retina",
                    DateOfBirth = new DateTime(1983, 7, 9),
                    IdentificationNumber = "445454"
                };
            }

            Patient patientLucyLittle;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloLucy", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                patientLucyLittle = new Patient
                {
                    Id = Guid.NewGuid(),
                    Username = "Lucy",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Lucy",
                    Surname = "Little",
                    DateOfBirth = new DateTime(1990, 1, 2),
                    IdentificationNumber = "121233"
                };
            }

            Patient patientMattTuskon;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloMatt", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                patientMattTuskon = new Patient
                {
                    Id = Guid.NewGuid(),
                    Username = "Matt",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Matt",
                    Surname = "Tuskon",
                    DateOfBirth = new DateTime(1991, 12, 3),
                    IdentificationNumber = "982456"
                };
            }

            Patient patientMartinRusko;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloMartin", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                patientMartinRusko = new Patient
                {
                    Id = Guid.NewGuid(),
                    Username = "Martin",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Martin",
                    Surname = "Rusko",
                    DateOfBirth = new DateTime(1980, 11, 11),
                    IdentificationNumber = "201305"
                };
            }

            Patient patientTrudyUrmanski;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloTrudy", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                patientTrudyUrmanski = new Patient
                {
                    Id = Guid.NewGuid(),
                    Username = "Trudy",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Trudy",
                    Surname = "Urmanski",
                    DateOfBirth = new DateTime(1977, 4, 12),
                    IdentificationNumber = "101077"
                };
            }

            Patient patientArthurMcCurtny;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloArthur", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                patientArthurMcCurtny = new Patient
                {
                    Id = Guid.NewGuid(),
                    Username = "Arthur",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Arthur",
                    Surname = "McCurtny",
                    DateOfBirth = new DateTime(2000, 6, 6),
                    IdentificationNumber = "798462"
                };
            }

            Patient patientJuliePuth;
            using (var deriveBytes = new Rfc2898DeriveBytes("helloJulie", saltSize, PBKDF2IterCount))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
                patientJuliePuth = new Patient
                {
                    Id = Guid.NewGuid(),
                    Username = "Julie",
                    PasswordHash = Convert.ToBase64String(subkey),
                    PasswordSalt = Convert.ToBase64String(salt),
                    Name = "Julie",
                    Surname = "Puth",
                    DateOfBirth = new DateTime(2001, 10, 3),
                    IdentificationNumber = "459782"
                };
            }

            #endregion

            #region Sympthoms
            var sympthomImmobilized = new Sympthom
            {
                Id = Guid.NewGuid(),
                Name = "immobilized"
            };

            var sympthomFewer = new Sympthom
            {
                Id = Guid.NewGuid(),
                Name = "fewer"
            };

            var sympthomHeadache = new Sympthom
            {
                Id = Guid.NewGuid(),
                Name = "headache"
            };

            var sympthomVommiting = new Sympthom
            {
                Id = Guid.NewGuid(),
                Name = "vommiting"
            };

            var sympthomStomachache = new Sympthom
            {
                Id = Guid.NewGuid(),
                Name = "stomachache"
            };

            var sympthomItchiness = new Sympthom
            {
                Id = Guid.NewGuid(),
                Name = "itchiness"
            };

            var sympthomCough = new Sympthom
            {
                Id = Guid.NewGuid(),
                Name = "cough"
            };

            var sympthomAnemia = new Sympthom
            {
                Id = Guid.NewGuid(),
                Name = "anemia"
            };

            var sympthomSneezing = new Sympthom
            {
                Id = Guid.NewGuid(),
                Name = "sneezing"
            };

            #endregion

            #region Diseases

            var diseaseParalyzed = new Disease
            {
                Id = Guid.NewGuid(),
                Name = "Paralyzed",
            };

            var diseaseFlu = new Disease
            {
                Id = Guid.NewGuid(),
                Name = "Flu",
            };

            var diseaseCold = new Disease
            {
                Id = Guid.NewGuid(),
                Name = "Cold",
            };

            var diseaseAnemiaIllness = new Disease
            {
                Id = Guid.NewGuid(),
                Name = "AnemiaIllness",
            };

            var diseaseSmallpox = new Disease
            {
                Id = Guid.NewGuid(),
                Name = "Smallpox",
            };

            var diseaseDiarrhea = new Disease
            {
                Id = Guid.NewGuid(),
                Name = "Diarrhea",
            };

            var diseaseHernia = new Disease
            {
                Id = Guid.NewGuid(),
                Name = "Hernia",
            };

            var diseaseSalmonela = new Disease
            {
                Id = Guid.NewGuid(),
                Name = "Salmonela",
            };

            #endregion

            var rickHealthCard = new HealthCard
            {
                Id = patientRickUnseen.Id,
                Patient = patientRickUnseen,
                BloodType = Enums.BloodType.B
            };

            MatchDiseaseAndHealthCard(diseaseSalmonela, rickHealthCard, context);
            patientRickUnseen.HealthCard = rickHealthCard;

            #region Disease and sympthom matching

            MatchDiseaseAndSympthom(diseaseParalyzed, sympthomImmobilized, context);
            MatchDiseaseAndSympthom(diseaseAnemiaIllness, sympthomAnemia, context);
            MatchDiseaseAndSympthom(diseaseDiarrhea, sympthomStomachache, context);
            MatchDiseaseAndSympthom(diseaseFlu, sympthomHeadache, context);
            MatchDiseaseAndSympthom(diseaseFlu, sympthomFewer, context);
            MatchDiseaseAndSympthom(diseaseFlu, sympthomCough, context);
            MatchDiseaseAndSympthom(diseaseFlu, sympthomSneezing, context);
            MatchDiseaseAndSympthom(diseaseHernia, sympthomStomachache, context);
            MatchDiseaseAndSympthom(diseaseSmallpox, sympthomItchiness, context);
            MatchDiseaseAndSympthom(diseaseCold, sympthomSneezing, context);
            MatchDiseaseAndSympthom(diseaseCold, sympthomFewer, context);
            MatchDiseaseAndSympthom(diseaseSalmonela, sympthomVommiting, context);

            #endregion

            #region Doctors and patients matching

            MatchDoctorAndPatient(doctorJohnDoe, patientHarryMaybourne, context);
            MatchDoctorAndPatient(doctorJohnDoe, patientRickUnseen, context);
            MatchDoctorAndPatient(doctorMacKane, patientHarryMaybourne, context);
            MatchDoctorAndPatient(doctorJimmyFella, patientTrudyUrmanski, context);
            MatchDoctorAndPatient(doctorJimmyFella, patientLucyLittle, context);
            MatchDoctorAndPatient(doctorMatildaBrown, patientJuliePuth, context);
            MatchDoctorAndPatient(doctorMatildaBrown, patientArthurMcCurtny, context);
            MatchDoctorAndPatient(doctorOliverGernitzky, patientMattTuskon, context);
            MatchDoctorAndPatient(doctorOliverGernitzky, patientLucyLittle, context);
            MatchDoctorAndPatient(doctorOliverGernitzky, patientRickUnseen, context);
            MatchDoctorAndPatient(doctorOliverGernitzky, patientTrudyUrmanski, context);
            MatchDoctorAndPatient(doctorPeterParker, patientArthurMcCurtny, context);
            MatchDoctorAndPatient(doctorPeterParker, patientHarryMaybourne, context);
            MatchDoctorAndPatient(doctorSamirRator, patientMattTuskon, context);
            MatchDoctorAndPatient(doctorSamirRator, patientMartinRusko, context);
            MatchDoctorAndPatient(doctorSamirRator, patientJulianRetina, context);
            MatchDoctorAndPatient(doctorVictoriaTerace, patientJulianRetina, context);
            MatchDoctorAndPatient(doctorVictoriaTerace, patientLucyLittle, context);

            #endregion

            #region Context update

            context.Sympthoms.AddOrUpdate(sympthomImmobilized);
            context.Sympthoms.AddOrUpdate(sympthomVommiting);
            context.Sympthoms.AddOrUpdate(sympthomStomachache);
            context.Sympthoms.AddOrUpdate(sympthomSneezing);
            context.Sympthoms.AddOrUpdate(sympthomItchiness);
            context.Sympthoms.AddOrUpdate(sympthomHeadache);
            context.Sympthoms.AddOrUpdate(sympthomFewer);
            context.Sympthoms.AddOrUpdate(sympthomCough);
            context.Sympthoms.AddOrUpdate(sympthomAnemia);

            context.Diseases.AddOrUpdate(diseaseAnemiaIllness);
            context.Diseases.AddOrUpdate(diseaseCold);
            context.Diseases.AddOrUpdate(diseaseDiarrhea);
            context.Diseases.AddOrUpdate(diseaseFlu);
            context.Diseases.AddOrUpdate(diseaseHernia);
            context.Diseases.AddOrUpdate(diseaseSalmonela);
            context.Diseases.AddOrUpdate(diseaseSmallpox);
            context.Diseases.AddOrUpdate(diseaseParalyzed);

            context.Users.AddOrUpdate(doctorJohnDoe);
            context.Users.AddOrUpdate(doctorMacKane);
            context.Users.AddOrUpdate(doctorJimmyFella);
            context.Users.AddOrUpdate(doctorMatildaBrown);
            context.Users.AddOrUpdate(doctorOliverGernitzky);
            context.Users.AddOrUpdate(doctorPeterParker);
            context.Users.AddOrUpdate(doctorSamirRator);
            context.Users.AddOrUpdate(doctorVictoriaTerace);

            context.Users.AddOrUpdate(patientHarryMaybourne);
            context.Users.AddOrUpdate(patientRickUnseen);
            context.Users.AddOrUpdate(patientArthurMcCurtny);
            context.Users.AddOrUpdate(patientJulianRetina);
            context.Users.AddOrUpdate(patientJuliePuth);
            context.Users.AddOrUpdate(patientLucyLittle);
            context.Users.AddOrUpdate(patientMartinRusko);
            context.Users.AddOrUpdate(patientMattTuskon);
            context.Users.AddOrUpdate(patientTrudyUrmanski);

            context.HealthCards.AddOrUpdate(rickHealthCard);

            #endregion

            context.SaveChanges();
            base.Seed(context);
        }

        private void MatchDiseaseAndHealthCard(Disease disease, HealthCard healthCard, HospitalISDbContext context)
        {
            var diseaseToHealthCard  = new DiseaseToHealthCard();
            diseaseToHealthCard.Id = Guid.NewGuid();
            diseaseToHealthCard.HealthCardId = healthCard.Id;
            diseaseToHealthCard.DiseaseId = disease.Id;
            disease.DiseaseToHealthCard.Add(diseaseToHealthCard);
            healthCard.DiseaseToHealthCard.Add(diseaseToHealthCard);
            context.DiseaseToHealthCard.AddOrUpdate(diseaseToHealthCard);
        }

        private void MatchDoctorAndPatient(Doctor doctor, Patient patient, HospitalISDbContext context)
        {
            DoctorToPatient dtp = new DoctorToPatient();
            dtp.Id = Guid.NewGuid();
            dtp.DoctorId = doctor.Id;
            dtp.PatientId = patient.Id;
            doctor.DoctorToPatients.Add(dtp);
            patient.DoctorToPatients.Add(dtp);
            context.DoctorToPatient.AddOrUpdate(dtp);
        }

        private void MatchDiseaseAndSympthom(Disease disease, Sympthom sympthom, HospitalISDbContext context)
        {
            DiseaseToSympthom dts = new DiseaseToSympthom();
            dts.Id = Guid.NewGuid();
            dts.SympthomId = sympthom.Id;
            dts.DiseaseId = disease.Id;
            disease.DiseaseToSympthoms.Add(dts);
            sympthom.DiseaseToSympthoms.Add(dts);
            context.DiseaseToSymptom.AddOrUpdate(dts);
        }
    }
}
