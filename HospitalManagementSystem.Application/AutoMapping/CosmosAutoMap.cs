using AutoMapper;
using HospitalManagementSystem.Application.DTO.CosmosDB;
using HospitalManagementSystem.Domain.Entities.CosmosDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.AutoMapping
{
    public class CosmosAutoMap : Profile
    {
        public CosmosAutoMap()
        {
            CreateMap<CosmosCreatePatientDTO, CosmosPatient>();
            CreateMap<CosmosUpdatePatientDTO, CosmosPatient>();
            CreateMap<CosmosPatient, CosmosPatientDTO>();
            CreateMap<CosmosCreatePatientDTO, CosmosPatient>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid().ToString()));
        }
    }
}
