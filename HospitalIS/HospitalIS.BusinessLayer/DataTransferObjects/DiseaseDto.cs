using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.DataTransferObjects
{
    public class DiseaseDto : DtoBase
    {
        public String Name { get; set; }

        public ICollection<DiseaseToSympthomDto> DiseaseToSympthomDtos { get; set; }

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
            return obj.GetType() == this.GetType() && ((DiseaseDto)obj).GetHashCode() == this.GetHashCode();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ Id.GetHashCode();
                return hashCode;
            }
        }
    }
}
