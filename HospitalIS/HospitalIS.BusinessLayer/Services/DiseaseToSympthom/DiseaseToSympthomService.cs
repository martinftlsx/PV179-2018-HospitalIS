using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.Services.Common;
using HospitalIS.Infrastructure;
using HospitalISDBContext.Entities;

namespace HospitalIS.BusinessLayer.Services.DiseaseToSympthom
{
    public class DiseaseToSympthomService : ServiceBase, IDiseaseToSympthomService
    {
        private IRepository<Disease> diseaseRepository;
        private IRepository<Sympthom> sympthomRepository;
        private IRepository<HospitalISDBContext.Entities.DiseaseToSympthom> diseaseToSympthomRepository;

        public DiseaseToSympthomService(IMapper mapper, IRepository<Disease> diseaseRepository, IRepository<Sympthom> sympthomRepository, IRepository<HospitalISDBContext.Entities.DiseaseToSympthom> diseaseToSympthomRepository) : base(mapper)
        {
            this.diseaseRepository = diseaseRepository;
            this.sympthomRepository = sympthomRepository;
            this.diseaseToSympthomRepository = diseaseToSympthomRepository;
        }

        public Guid Create(Guid diseaseGuid, Guid sympthomGuid)
        {
            HospitalISDBContext.Entities.DiseaseToSympthom diseaseToSympthom =
                new HospitalISDBContext.Entities.DiseaseToSympthom();
            diseaseToSympthom.DiseaseId = diseaseGuid;
            diseaseToSympthom.SympthomId = sympthomGuid;
            return diseaseToSympthomRepository.Create(diseaseToSympthom);
        }
    }
}
