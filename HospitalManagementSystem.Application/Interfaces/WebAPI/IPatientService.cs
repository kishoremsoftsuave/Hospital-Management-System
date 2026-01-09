using HospitalManagementSystem.Application.DTO.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces.WebAPI
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
