using AutoMapper;
using HospitalManagementSystem.Application.DTO.ElasticSearch;
using HospitalManagementSystem.Application.Interfaces.ElasticSearch;
using HospitalManagementSystem.Domain.Entities.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Services.ElasticSearch
{
    public class ElasticHospitalService : IElasticHospitalService
    {
        private readonly IElasticHospitalRepository _repo;
        private readonly IMapper _mapper;
        public ElasticHospitalService(IElasticHospitalRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ElasticHospitalDTO>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<ElasticHospitalDTO?> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task Create(ElasticHospitalDTO hospitaldto)
        {
            await _repo.Create(hospitaldto);
        }

        public async Task Update(int id, ElasticHospitalDTO hospitaldto)
        {
            var hospital = await _repo.GetById(id);
            if(hospital is null) 
                throw new KeyNotFoundException($"Hospital with id {id} not found.");
            await _repo.Update(id, hospitaldto);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }
    }
}
