using HospitalIS.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalISDBContext.Entities
{
    public class Sympthom : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(HospitalISDbContext.Sympthoms);

        [Required]
        [MaxLength(64)]
        public String Name { get; set; }

        public virtual ICollection<DiseaseToSympthom> DiseaseToSympthoms { get; set; } = new HashSet<DiseaseToSympthom>();
    }
}
