using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Domain.Entities;
using AutoMapper;

namespace HospitalManagementSystem.Application.AutoMapping
{
    public class AutoMap : Profile
    {
        public AutoMap() 
        {
            CreateMap<Hospital, HospitalDTO>().ReverseMap();
            CreateMap<Doctor, DoctorDTO>().ReverseMap();
        } 
    }
}
