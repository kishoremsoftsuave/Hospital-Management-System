using HospitalManagementSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces
{
    public interface IDoctorService 
    {
        Task<List<DoctorDetailDTO>> GetAll();
        Task<DoctorDetailDTO> GetById(int id);
        Task Create(DoctorDTO doctorDTO);
        Task Update(int id, DoctorDTO doctorDTO);
        Task Delete(int id);
    }
}
