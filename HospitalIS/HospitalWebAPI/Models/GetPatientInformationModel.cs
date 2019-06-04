using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalWebAPI.Models
{
    public class GetPatientInformationModel
    {
        [Required]
        [MaxLength(64)]
        [MinLength(2)]
        public string IdentificationNumber { get; set; }
    }
}