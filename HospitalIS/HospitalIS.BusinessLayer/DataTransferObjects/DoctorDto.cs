using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using System.Collections.Generic;
using HospitalISDBContext.Enums;

namespace HospitalIS.BusinessLayer.DataTransferObjects
{
    public class DoctorDto : DtoBase
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public string PasswordHash { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Specialization Specialization { get; set; }

        public IEnumerable<PatientDto> PatientDtos { get; set; }

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
            return obj.GetType() == this.GetType() && ((DoctorDto)obj).GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();

                return hashCode;
            }
        }
    }
}
