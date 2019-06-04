using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using HospitalISDBContext.Entities;
using System;

namespace HospitalIS.BusinessLayer.DataTransferObjects.Filters
{
    public class HealthCardFilterDto : FilterDtoBase
    {
        public Guid PatientId { get; set; }
    }
}
