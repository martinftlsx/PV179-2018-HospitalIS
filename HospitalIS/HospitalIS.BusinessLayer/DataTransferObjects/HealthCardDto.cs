using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;
using HospitalISDBContext.Enums;

namespace HospitalIS.BusinessLayer.DataTransferObjects
{
    public class HealthCardDto : DtoBase
    {
        public BloodType BloodType { get; set; }

        public DateTime LastUpdate { get; set; }

        public PatientDto PatientDto { get; set; }

        public ICollection<DiseaseToHealthCardDto> DiseaseToHealthCardDtos { get; set; }

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
            return obj.GetType() == this.GetType() && ((HealthCardDto)obj).GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)BloodType;
                hashCode = (hashCode * 397) ^ Id.GetHashCode();
                hashCode = (hashCode * 397) ^ LastUpdate.GetHashCode();

                return hashCode;
            }
        }
    }
}
