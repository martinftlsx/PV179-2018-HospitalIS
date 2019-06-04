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
    public class DiseaseToHealthCard : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(DiseaseToHealthCard);

        [ForeignKey("Disease")]
        public Guid DiseaseId { get; set; }

        public virtual Disease Disease { get; set; }

        [ForeignKey("HealthCard")]
        public Guid HealthCardId { get; set; }

        public virtual HealthCard HealthCard { get; set; }
    }
}
