using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HospitalIS.BusinessLayer.DataTransferObjects;
using HospitalIS.BusinessLayer.DataTransferObjects.Common;
using HospitalIS.BusinessLayer.DataTransferObjects.Filters;
using HospitalIS.Infrastructure.Query;
using HospitalISDBContext.Entities;

namespace HospitalIS.BusinessLayer.Config
{
    public class MappingConfig
    {
        public static void ConfigureMapping(IMapperConfigurationExpression config)
        {
            config.CreateMap<Disease, DiseaseDto>()
                .ForMember(dest => dest.DiseaseToSympthomDtos, opt => opt.MapFrom(src => src.DiseaseToSympthoms))
                .ReverseMap();
            config.CreateMap<DiseaseToSympthom, DiseaseToSympthomDto>().ReverseMap();
            config.CreateMap<DiseaseToHealthCard, DiseaseToHealthCardDto>().ReverseMap();
            config.CreateMap<DoctorToPatient, DoctorToPatientDto>().ReverseMap();
            config.CreateMap<Doctor, DoctorDto>();
            config.CreateMap<DoctorDto, Doctor>()
                .ForMember(dest => dest.DoctorToPatients, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AccessRights, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());
            config.CreateMap<HealthCard, HealthCardDto>()
                .ForMember(dest => dest.PatientDto, opt => opt.MapFrom(src => src.Patient))
                .ForMember(dest => dest.DiseaseToHealthCardDtos, opt => opt.MapFrom(src => src.DiseaseToHealthCard));
            config.CreateMap<HealthCardDto, HealthCard>()
                .ForMember(dest => dest.Patient, opt => opt.MapFrom(src => src.PatientDto))
                .ForMember(dest => dest.DiseaseToHealthCard, opt => opt.Ignore());
            config.CreateMap<Patient, PatientDto>();
            config.CreateMap<PatientDto, Patient>()
                .ForMember(dest => dest.DoctorToPatients, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AccessRights, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());
            config.CreateMap<Sympthom, SympthomDto>()
                .ForMember(dest => dest.DiseaseToSympthomDtos, opt => opt.MapFrom(src => src.DiseaseToSympthoms))
                .ReverseMap();
            config.CreateMap<QueryResult<Disease>, QueryResultDto<DiseaseDto, DiseaseFilterDto>>();
            config.CreateMap<QueryResult<Doctor>, QueryResultDto<DoctorDto, DoctorFilterDto>>();
            config.CreateMap<QueryResult<HealthCard>, QueryResultDto<HealthCardDto, HealthCardFilterDto>>();
            config.CreateMap<QueryResult<Patient>, QueryResultDto<PatientDto, PatientFilterDto>>();
            config.CreateMap<QueryResult<Sympthom>, QueryResultDto<SympthomDto, SympthomFilterDto>>();
            config.CreateMap<QueryResult<DoctorToPatient>, QueryResultDto<DoctorToPatientDto, DoctorToPatientFilterDto>>();
        }

    }
}
