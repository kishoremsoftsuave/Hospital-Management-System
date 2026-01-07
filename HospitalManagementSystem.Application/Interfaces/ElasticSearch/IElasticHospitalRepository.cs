using HospitalManagementSystem.Application.DTO.ElasticSearch;
using HospitalManagementSystem.Domain.Entities.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Interfaces.ElasticSearch
{
    public interface IElasticHospitalRepository
    {
        Task<IEnumerable<ElasticHospitalDetailDTO>> GetAll();
        Task<ElasticHospitalDetailDTO?> GetById(Guid id);
        Task Create(ElasticHospitalCreateDTO hospitalDto);
        Task Update(Guid id, ElasticHospitalDetailDTO hospitalDto);
        Task Delete(Guid id);
    }
}
