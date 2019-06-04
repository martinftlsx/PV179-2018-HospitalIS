using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.Services.DiseaseToSympthom
{
    public interface IDiseaseToSympthomService
    {
        Guid Create(Guid diseaseGuid, Guid sympthomGuid);
    }
}
