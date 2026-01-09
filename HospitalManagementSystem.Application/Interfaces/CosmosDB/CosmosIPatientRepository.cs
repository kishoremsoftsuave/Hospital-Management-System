using HospitalManagementSystem.Domain.Entities.CosmosDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces.CosmosDB
{
    public interface CosmosIPatientRepository
    {
        Task<List<CosmosPatient>> GetAll();
        Task<CosmosPatient?> GetById(string id);
        Task<CosmosPatient> Create(CosmosPatient patient);
        Task<CosmosPatient> UpdateById(CosmosPatient patient);
        Task Delete(string id); 
    }
}
