using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.DataTransferObjects
{
    public class DiseaseToHealthCardDto : DtoBase
    {
        public DiseaseDto DiseaseDto { get; set; }
        public HealthCardDto HealthCardDto { get; set; }
    }
}
