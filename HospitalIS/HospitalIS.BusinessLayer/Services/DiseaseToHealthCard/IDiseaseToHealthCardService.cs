using System;

namespace HospitalIS.BusinessLayer.Services.DiseaseToHealthCard
{
    public  interface IDiseaseToHealthCardService
    {
        Guid Create(Guid diseaseGuid, Guid healthCardGuid);
    }
}