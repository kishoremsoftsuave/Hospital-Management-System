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
            CreateMap<CosmosCreatePatientDTO, CosmosPatient>().ReverseMap();
            CreateMap<CosmosUpdatePatientDTO, CosmosPatient>().ReverseMap();
            CreateMap<CosmosPatient, CosmosPatientDTO>().ReverseMap();
        }
    }
}
