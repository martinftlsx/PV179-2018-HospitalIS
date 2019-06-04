using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalISDBContext.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalIS.WebApplication.Models.HealthCard
{
    public class HealthCardUpdateModel
    {
        public BloodType BloodType { get; set; }

        public Guid DiseaseId { get; set; }
    }
}