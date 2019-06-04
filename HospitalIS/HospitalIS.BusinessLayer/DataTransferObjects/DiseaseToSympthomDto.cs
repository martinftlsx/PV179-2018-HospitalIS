using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalIS.BusinessLayer.DataTransferObjects.Common;

namespace HospitalIS.BusinessLayer.DataTransferObjects
{
    public class DiseaseToSympthomDto : DtoBase
    {
        public DiseaseDto DiseaseDto { get; set; }
        public SympthomDto SympthomDto { get; set; }
    }
}
