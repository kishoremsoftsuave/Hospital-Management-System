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
        Task<Guid> Create(ElasticHospitalCreateDTO hospitaldto);
        Task Update(Guid id, ElasticHospitalUpdateDTO hospitaldto);
        Task Delete(Guid id);
    }
}
