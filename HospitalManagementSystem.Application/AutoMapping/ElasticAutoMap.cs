using AutoMapper;
using HospitalManagementSystem.Application.DTO.ElasticSearch;
using HospitalManagementSystem.Domain.Entities.ElasticSearch;

public class ElasticAutoMap : Profile
{
    public ElasticAutoMap()
    {
        // Entity → DetailDTO
        CreateMap<ElasticHospital, ElasticHospitalDetailDTO>()
            .ForMember(dest => dest.HospitalName, opt => opt.MapFrom(src => src.Name));

        // CreateDTO → Entity
        CreateMap<ElasticHospitalCreateDTO, ElasticHospital>().ReverseMap();

        // UpdateDTO → Entity
        CreateMap<ElasticHospitalUpdateDTO, ElasticHospital>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
