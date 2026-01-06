using HospitalManagementSystem.Application.DTO.ElasticSearch;
using HospitalManagementSystem.Domain.Entities.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces.ElasticSearch
{
    public interface IElasticHospitalRepository
    {
        Task<IEnumerable<ElasticHospitalDTO>> GetAll();
        Task<ElasticHospitalDTO?> GetById(int id);
        Task Create(ElasticHospitalDTO hospitalDto);
        Task Update(int id, ElasticHospitalDTO hospitalDto);
        Task Delete(int id);
    }
}
