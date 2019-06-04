using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HospitalISDBContext.Enums;

namespace HospitalWebAPI.Models
{
    public class ReturnPatientInformationModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string IdentificationNumber { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime ProfileCreationDate { get; set; }

        public IEnumerable<Tuple<string, Specialization>> Doctors { get; set; }
    }
}