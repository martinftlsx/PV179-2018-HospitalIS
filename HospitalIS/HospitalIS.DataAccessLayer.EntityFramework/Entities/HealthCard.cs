using HospitalIS.Infrastructure;
using HospitalISDBContext.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalISDBContext.Entities
{
    public class HealthCard : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(HospitalISDbContext.Diseases);

        [Required]
        public BloodType BloodType { get; set; }

        public DateTime LastUpdate { get; set; } = new DateTime(1990, 12, 12);

        public virtual Patient Patient { get; set; }

        public virtual ICollection<DiseaseToHealthCard> DiseaseToHealthCard { get; set; } = new HashSet<DiseaseToHealthCard>();
    }
}
