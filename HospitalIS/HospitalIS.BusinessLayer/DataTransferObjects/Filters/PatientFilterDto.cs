using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.DataTransferObjects.Filters
{
    public class PatientFilterDto : FilterDtoBase
    {
        public string Username;

        public string FullName { get; set; }

        public string IdentificationNumber { get; set; }
    }
}
