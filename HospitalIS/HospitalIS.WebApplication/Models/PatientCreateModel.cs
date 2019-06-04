using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HospitalISDBContext.Enums;

namespace HospitalIS.WebApplication.Models
{
    public class PatientCreateModel
    {
        [Required]
        [StringLength(64, ErrorMessage = "Username length must be between 2 and 64", MinimumLength = 2)]
        public string Username { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Password length must be between 8 and 64", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Name length must be between 1 and 64", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Surname length must be between 1 and 64", MinimumLength = 2)]
        public string Surname { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Surname length must be between 6 and 64", MinimumLength = 2)]
        public string IdentificationNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; } = new DateTime(1990, 12, 12);

        [DataType(DataType.Date)]
        public DateTime ProfileCreationDate { get; set; } = new DateTime(1990, 12, 12);
    }
}