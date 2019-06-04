using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.DataTransferObjects.Filters
{
    public class DoctorToPatientFilterDto : FilterDtoBase
    {
        public Guid DoctorId { get; set; }

        public Guid PatientId { get; set; }
    }
}
