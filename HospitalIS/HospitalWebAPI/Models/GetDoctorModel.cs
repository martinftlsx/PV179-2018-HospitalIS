using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class GetDoctorModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Specialization { get; set; }
    }
}