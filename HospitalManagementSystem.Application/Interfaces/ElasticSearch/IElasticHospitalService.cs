using HospitalManagementSystem.Application.DTO.ElasticSearch;
using HospitalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces.ElasticSearch
{
    public interface IElasticHospitalService
    {
        Task<IEnumerable<ElasticHospitalDetailDTO>> GetAll();
        Task<ElasticHospitalDetailDTO?> GetById(Guid id);
        Task Create(ElasticHospitalCreateDTO hospitaldto);
        Task Update(Guid id, ElasticHospitalDetailDTO hospitaldto);
        Task Delete(Guid id);
    }
}
