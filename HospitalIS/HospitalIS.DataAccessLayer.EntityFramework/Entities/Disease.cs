using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HospitalIS.Infrastructure;

namespace HospitalISDBContext.Entities
{
    public class Disease : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(HospitalISDbContext.Diseases);

        [Required]
        [MaxLength(64)]
        public String Name { get; set; }

        public virtual ICollection<DiseaseToSympthom> DiseaseToSympthoms { get; set; } = new HashSet<DiseaseToSympthom>();
        public virtual ICollection<DiseaseToHealthCard> DiseaseToHealthCard { get; set; } = new HashSet<DiseaseToHealthCard>();
    }
}
