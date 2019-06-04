using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalISDBContext.Enums;

namespace HospitalISDBContext.Entities
{
    public class Patient : User
    {
        [Required]
        public string IdentificationNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; } = new DateTime(1990, 12, 12);

        public DateTime ProfileCreationDate { get; set; } = new DateTime(1990, 12, 12);

        public virtual HealthCard HealthCard { get; set; }

        public virtual ICollection<DoctorToPatient> DoctorToPatients { get; set; } = new HashSet<DoctorToPatient>();

        public Patient() : base()
        {
            this.AccessRights = AccessRights.Patient;
        }
    }
}
