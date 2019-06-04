using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalISDBContext.Enums;

namespace HospitalIS.WebApplication.Models
{
    public class PatientInfoModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdentificationNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime ProfileCreationDate { get; set; }
        public BloodType BloodType { get; set; }
        public IEnumerable<DoctorDto> Doctors { get; set; }
        public IEnumerable<DiseaseDto> Diseases { get; set; }
    }
}