using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HospitalISDBContext.Enums;

namespace HospitalIS.WebApplication.Models
{
    public class DoctorCreateModel
    {
        [Required]
        [StringLength(64, ErrorMessage = "Username length must be between 2 and 64", MinimumLength = 2)]
        public string Username { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Password length must be between 8 and 64", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        public Specialization Specialization { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Name length must be between 1 and 64", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Surname length must be between 1 and 64", MinimumLength = 2)]
        public string Surname { get; set; }
    }
}