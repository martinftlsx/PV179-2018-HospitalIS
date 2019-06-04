using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.DataTransferObjects
{
    public class DoctorToPatientDto : DtoBase
    {
        public DoctorDto DoctorDto { get; set; }

        public PatientDto PatientDto { get; set; }
    }
}
