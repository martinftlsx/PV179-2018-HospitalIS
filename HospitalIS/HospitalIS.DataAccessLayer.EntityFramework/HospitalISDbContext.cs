using HospitalISDBContext.Entities;
using System.Data.Common;
using System.Data.Entity;

namespace HospitalISDBContext
{
    public class HospitalISDbContext : DbContext
    {
        private const string ConnectionString = "Data source=(localdb)\\mssqllocaldb;Database=HospitalISDbContext;Trusted_Connection=True;MultipleActiveResultSets=true";

        public HospitalISDbContext() : base(ConnectionString)
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Database.SetInitializer(new HospitalISInitializer());
        }

        public HospitalISDbContext(DbConnection connection) : base(connection, true)
        {
            Database.CreateIfNotExists();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Doctors");
            });

            modelBuilder.Entity<Patient>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Patients");
            });

            //Configure Id of patient as FK for HealthCard

            modelBuilder.Entity<Patient>()
                .HasRequired(patient => patient.HealthCard)
                .WithRequiredPrincipal(card => card.Patient);

            //modelBuilder.Entity<Patient>()
            //    .HasOptional(p => p.HealthCard)
            //    .WithRequired(healthCard => healthCard.Patient);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<HealthCard> HealthCards { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<DiseaseToSympthom> DiseaseToSymptom { get; set; }
        public DbSet<DiseaseToHealthCard> DiseaseToHealthCard { get; set; }
        public DbSet<DoctorToPatient> DoctorToPatient { get; set; }
        public DbSet<Sympthom> Sympthoms { get; set; }
    }
}
