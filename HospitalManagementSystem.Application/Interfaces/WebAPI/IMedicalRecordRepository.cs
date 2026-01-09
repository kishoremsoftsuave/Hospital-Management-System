using HospitalManagementSystem.Domain.Entities.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces.WebAPI
{
    public interface IMedicalRecordRepository
    {
        Task<List<MedicalRecord>> GetAll();
        Task<MedicalRecord?> GetById(int id);
        Task Create(MedicalRecord medicalRecord);
        Task Update(MedicalRecord medicalRecord);
        Task Delete(int id);
    }
}
