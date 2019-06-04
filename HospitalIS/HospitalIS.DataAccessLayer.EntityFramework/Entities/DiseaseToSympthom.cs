using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalIS.Infrastructure;

namespace HospitalISDBContext.Entities
{
    public class DiseaseToSympthom : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(DiseaseToSympthom);

        [ForeignKey("Disease")]
        public Guid DiseaseId { get; set; }

        public virtual Disease Disease { get; set; }

        [ForeignKey("Sympthom")]
        public Guid SympthomId { get; set; }

        public virtual Sympthom Sympthom { get; set; }
    }
}
