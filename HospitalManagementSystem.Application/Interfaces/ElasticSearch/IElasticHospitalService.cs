using HospitalManagementSystem.Application.DTO.ElasticSearch;
using HospitalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces.ElasticSearch
{
    public interface IElasticHospitalService
    {
        Task<IEnumerable<ElasticHospitalDTO>> GetAll();
        Task<ElasticHospitalDTO?> GetById(int id);
        Task Create(ElasticHospitalDTO hospitaldto);
        Task Update(int id, ElasticHospitalDTO hospitaldto);
        Task Delete(int id);
    }
}
