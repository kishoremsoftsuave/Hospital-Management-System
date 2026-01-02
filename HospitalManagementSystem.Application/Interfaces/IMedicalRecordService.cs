using HospitalManagementSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces
{
    public interface IMedicalRecordService
    {
        Task<List<MedicalRecordDTO>> GetAll();
        Task<MedicalRecordDTO> GetById(int id);
        Task Create(MedicalRecordDTO medicalRecordDTO);
        Task Update(int id, MedicalRecordDTO medicalRecordDTO);
        Task Delete(int id);
    }
}
