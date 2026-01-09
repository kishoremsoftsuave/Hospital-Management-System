using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Domain.Entities.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces.WebAPI
{
    public interface IDoctorRepository
    {
        Task<List<Doctor>> GetAll();
        Task<Doctor?> GetById(int id);
        Task Create(Doctor doctor);
        Task Update(Doctor doctor);
        Task Delete(int id);
    }
}
