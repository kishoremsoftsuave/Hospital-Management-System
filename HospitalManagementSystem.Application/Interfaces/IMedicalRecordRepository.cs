using HospitalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces
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
