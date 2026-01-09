using HospitalManagementSystem.Domain.Entities.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces.WebAPI
{
    public interface IPrescriptionRepository
    {
        Task<List<Prescription>> GetAll();
        Task<Prescription?> GetById(int id);
        Task Create(Prescription prescription);
        Task Update(Prescription prescription);
        Task Delete(int id);
    }
}
