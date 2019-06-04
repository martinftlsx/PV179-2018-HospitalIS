using AutoMapper;
using HospitalIS.BusinessLayer.Services.Common;
using HospitalIS.Infrastructure;
using HospitalISDBContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalIS.BusinessLayer.Services.DiseaseToHealthCard
{
    public class DiseaseToHealthCardService : ServiceBase, IDiseaseToHealthCardService
    {
        private readonly IRepository<HospitalISDBContext.Entities.DiseaseToHealthCard> diseaseToHealthCardRepository;

        public DiseaseToHealthCardService(IMapper mapper, IRepository<HospitalISDBContext.Entities.DiseaseToHealthCard> diseaseToHealthCardRepository) : base(mapper)
        {
            this.diseaseToHealthCardRepository = diseaseToHealthCardRepository;
        }

        public Guid Create(Guid diseaseGuid, Guid healthCardGuid)
        {
            HospitalISDBContext.Entities.DiseaseToHealthCard diseaseToHealthCard =
                new HospitalISDBContext.Entities.DiseaseToHealthCard
                {
                    DiseaseId = diseaseGuid,
                    HealthCardId = healthCardGuid
                };
            return diseaseToHealthCardRepository.Create(diseaseToHealthCard);
        }
    }
}
