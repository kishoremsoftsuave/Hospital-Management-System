using HospitalManagementSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces
{
    public interface IPatientService
    {
        Task<List<PatientDTO>> GetAll();
        Task<PatientDTO> GetById(int id);
        Task Create(PatientDTO patientDTO);
        Task Update(int id, PatientDTO patientDTO);
        Task Delete(int id);
    }
}
