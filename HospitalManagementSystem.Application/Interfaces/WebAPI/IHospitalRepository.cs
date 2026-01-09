using HospitalManagementSystem.Domain.Entities.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces.WebAPI
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
