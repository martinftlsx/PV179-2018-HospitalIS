using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class EditDoctorModel
    {
        [Required]
        public string Username { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Specialization { get; set; }
    }
}