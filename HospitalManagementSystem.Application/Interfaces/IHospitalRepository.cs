using HospitalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces
{
    public interface IHospitalRepository
    {
        Task<List<Hospital>> GetAll();
        Task<Hospital?> GetById(int id);
        Task Create(Hospital hospital);
        Task Update(Hospital hospital);
        Task Delete(int id);
    }
}
