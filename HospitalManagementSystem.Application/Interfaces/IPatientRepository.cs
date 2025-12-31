using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAll();
        Task<Patient?> GetById(int id);
        Task Create(Patient patient);
        Task Update(Patient patient);
        Task Delete(int id);
    }
}
