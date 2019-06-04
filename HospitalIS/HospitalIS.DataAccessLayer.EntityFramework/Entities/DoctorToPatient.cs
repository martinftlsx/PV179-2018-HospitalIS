using HospitalIS.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalISDBContext.Entities
{
    public class DoctorToPatient : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(DoctorToPatient);

        [ForeignKey("Doctor")]
        public Guid DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        [ForeignKey("Patient")]
        public Guid PatientId { get; set; }

        public virtual Patient Patient { get; set; }
    }
}
