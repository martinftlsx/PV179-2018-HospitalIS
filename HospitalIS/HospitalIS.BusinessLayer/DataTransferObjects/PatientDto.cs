using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;

namespace HospitalIS.BusinessLayer.DataTransferObjects
{
    public class PatientDto : DtoBase
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string IdentificationNumber { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime ProfileCreationDate { get; set; }

        public HealthCardDto HealthCard { get; set; }

        public ICollection<DoctorDto> Doctors { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj.GetType() == this.GetType() && ((PatientDto)obj).GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Id.GetHashCode();
                hashCode = (hashCode * 397) ^ Surname.GetHashCode();
                hashCode = (hashCode * 397) ^ IdentificationNumber.GetHashCode();
                hashCode = (hashCode * 397) ^ (Email?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ DateOfBirth.GetHashCode();
                hashCode = (hashCode * 397) ^ ProfileCreationDate.GetHashCode();
                return hashCode;
            }
        }
    }
}
