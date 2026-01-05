using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces
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
