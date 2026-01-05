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
            CreateMap<Patient, PatientDTO>().ReverseMap();
            CreateMap<Patient, PatientCreateDTO>().ReverseMap();
            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<MedicalRecord, MedicalRecordDTO>().ReverseMap();
            CreateMap<Prescription, PrescriptionDTO>().ReverseMap();
            CreateMap<Doctor, DoctorDetailDTO>()
                .ForMember(d => d.DoctorName,o => o.MapFrom(s => s.Name))
                .ForMember(d => d.HospitalName,o => o.MapFrom(s => s.Hospital.Name))
                .ForMember(d => d.HospitalAddress,o => o.MapFrom(s => s.Hospital.Location));

        }
    }
}
