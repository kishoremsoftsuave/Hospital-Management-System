using HospitalManagementSystem.Application.DTO.ElasticSearch;
using HospitalManagementSystem.Domain.Entities.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces.ElasticSearch
{
    public interface IElasticHospitalRepository
    {
        Task<IEnumerable<ElasticHospital>> GetAll();
        Task<ElasticHospital?> GetById(Guid id);
        Task Create(ElasticHospital hospital);
        Task Update(Guid id, ElasticHospital hospital);
        Task Delete(Guid id);
    }
}
