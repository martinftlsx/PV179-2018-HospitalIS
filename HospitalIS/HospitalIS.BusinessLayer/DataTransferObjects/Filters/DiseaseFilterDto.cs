using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.DataTransferObjects.Filters
{
    public class DiseaseFilterDto : FilterDtoBase
    {
        public string DiseaseName { get; set; }
    }
}
