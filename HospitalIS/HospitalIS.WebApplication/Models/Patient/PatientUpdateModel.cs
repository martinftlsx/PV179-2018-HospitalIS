using HospitalISDBContext.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalIS.WebApplication.Models.Patient
{
    public class PatientUpdateModel
    {
        [StringLength(64, ErrorMessage = "Username length must be between 1 and 64", MinimumLength = 1)]
        public string Username { get; set; }

        [Display(Name = "New name")]
        [StringLength(64, ErrorMessage = "Name length must be between 2 and 64", MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "New surname")]
        [StringLength(64, ErrorMessage = "Surname length must be between 2 and 64", MinimumLength = 2)]
        public string Surname { get; set; }

        [Display(Name = "New email")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        public string Email { get; set; }

    }
}