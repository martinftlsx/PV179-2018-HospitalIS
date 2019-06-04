namespace HospitalISDBContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HospitalFinalDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diseases",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DiseaseToHealthCards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DiseaseId = c.Guid(nullable: false),
                        HealthCardId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Diseases", t => t.DiseaseId, cascadeDelete: true)
                .ForeignKey("dbo.HealthCards", t => t.HealthCardId, cascadeDelete: true)
                .Index(t => t.DiseaseId)
                .Index(t => t.HealthCardId);
            
            CreateTable(
                "dbo.HealthCards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BloodType = c.Int(nullable: false),
                        LastUpdate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Patients", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.DoctorToPatients",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DoctorId = c.Guid(nullable: false),
                        PatientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .Index(t => t.DoctorId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.DiseaseToSympthoms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DiseaseId = c.Guid(nullable: false),
                        SympthomId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Diseases", t => t.DiseaseId, cascadeDelete: true)
                .ForeignKey("dbo.Sympthoms", t => t.SympthomId, cascadeDelete: true)
                .Index(t => t.DiseaseId)
                .Index(t => t.SympthomId);
            
            CreateTable(
                "dbo.Sympthoms",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 64),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(nullable: false, maxLength: 64),
                        PasswordSalt = c.String(nullable: false, maxLength: 100),
                        PasswordHash = c.String(nullable: false, maxLength: 100),
                        AccessRights = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 64),
                        Surname = c.String(nullable: false, maxLength: 64),
                        Specialization = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(nullable: false, maxLength: 64),
                        PasswordSalt = c.String(nullable: false, maxLength: 100),
                        PasswordHash = c.String(nullable: false, maxLength: 100),
                        AccessRights = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 64),
                        Surname = c.String(nullable: false, maxLength: 64),
                        IdentificationNumber = c.String(nullable: false),
                        Email = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        ProfileCreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DiseaseToSympthoms", "SympthomId", "dbo.Sympthoms");
            DropForeignKey("dbo.DiseaseToSympthoms", "DiseaseId", "dbo.Diseases");
            DropForeignKey("dbo.DiseaseToHealthCards", "HealthCardId", "dbo.HealthCards");
            DropForeignKey("dbo.HealthCards", "Id", "dbo.Patients");
            DropForeignKey("dbo.DoctorToPatients", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.DoctorToPatients", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.DiseaseToHealthCards", "DiseaseId", "dbo.Diseases");
            DropIndex("dbo.DiseaseToSympthoms", new[] { "SympthomId" });
            DropIndex("dbo.DiseaseToSympthoms", new[] { "DiseaseId" });
            DropIndex("dbo.DoctorToPatients", new[] { "PatientId" });
            DropIndex("dbo.DoctorToPatients", new[] { "DoctorId" });
            DropIndex("dbo.HealthCards", new[] { "Id" });
            DropIndex("dbo.DiseaseToHealthCards", new[] { "HealthCardId" });
            DropIndex("dbo.DiseaseToHealthCards", new[] { "DiseaseId" });
            DropTable("dbo.Patients");
            DropTable("dbo.Doctors");
            DropTable("dbo.Sympthoms");
            DropTable("dbo.DiseaseToSympthoms");
            DropTable("dbo.DoctorToPatients");
            DropTable("dbo.HealthCards");
            DropTable("dbo.DiseaseToHealthCards");
            DropTable("dbo.Diseases");
        }
    }
}
