using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HospitalISDBContext.Enums;

namespace HospitalIS.WebApplication.Models
{
    public class DoctorUpdateModel
    {
        [StringLength(64, ErrorMessage = "Username length must be between 1 and 64", MinimumLength = 1)]
        public string Username { get; set; }

        [Display(Name = "New password")]
        [StringLength(64, ErrorMessage = "Password length must be between 8 and 64", MinimumLength = 8)]
        public string NewPassword { get; set; }

        [Display(Name = "New specialization")]
        public Specialization Specialization { get; set; }

        [Display(Name = "New name")]
        [StringLength(64, ErrorMessage = "Name length must be between 2 and 64", MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "New surname")]
        [StringLength(64, ErrorMessage = "Surname length must be between 2 and 64", MinimumLength = 2)]
        public string Surname { get; set; }
    }
}