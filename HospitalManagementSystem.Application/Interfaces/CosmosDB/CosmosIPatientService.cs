using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.DTO.CosmosDB;
using HospitalManagementSystem.Domain.Entities.CosmosDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces.CosmosDB
{
    public interface CosmosIPatientService
    {
        Task<List<CosmosPatientDTO>> GetAll();
        Task<CosmosPatientDTO?> GetById(string id);
        Task<CosmosPatientDTO> Create(CosmosCreatePatientDTO dto);
        Task<CosmosPatientDTO> UpdateById(CosmosUpdatePatientDTO dto);
        Task Delete(string id);
    }
}
