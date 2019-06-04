using System.ComponentModel.DataAnnotations;

namespace HospitalWebAPI.Models
{
    public class CreateDoctorModel
    {
        [Required]
        [MaxLength(64)]
        [MinLength(2)]
        public string Username { get; set; }

        [Required]
        [MaxLength(64)]
        [MinLength(8)]
        public string Password { get; set; }
    
        [Required]
        [MaxLength(64)]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MaxLength(64)]
        [MinLength(2)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(64)]
        [MinLength(2)]
        public string Specialization { get; set; }
    }
}