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

        public async Task<IEnumerable<ElasticHospitalDetailDTO>> GetAll()
        {
            var hospital = await _repo.GetAll();
            return _mapper.Map<IEnumerable<ElasticHospitalDetailDTO>>(hospital);
        }

        public async Task<ElasticHospitalDetailDTO?> GetById(Guid id)
        {
            var hospital = await _repo.GetById(id);
            if(hospital is null)
                throw new KeyNotFoundException($"Hospital with id {id} not found.");
            return _mapper.Map<ElasticHospitalDetailDTO>(hospital);
        }

        public async Task<Guid> Create(ElasticHospitalCreateDTO hospitaldto)
        {
            var hospital = _mapper.Map<ElasticHospital>(hospitaldto);

            // Generate GUID
            hospital.Id = Guid.NewGuid();

            // Save to repository
            await _repo.Create(hospital);

            return hospital.Id;
        }

        public async Task Update(Guid id, ElasticHospitalUpdateDTO hospitaldto)
        {
            var hospital = await _repo.GetById(id);
            if (hospital is null)
                throw new KeyNotFoundException($"Hospital with id {id} not found.");
            var updatedHospital = _mapper.Map(hospitaldto,hospital);
            await _repo.Update(id, updatedHospital);
        }


        public async Task Delete(Guid id)
        {
            var hospital = await _repo.GetById(id);
            if (hospital is null)
                throw new KeyNotFoundException($"Hospital with id {id} not found.");
            await _repo.Delete(id);
        }
    }
}
