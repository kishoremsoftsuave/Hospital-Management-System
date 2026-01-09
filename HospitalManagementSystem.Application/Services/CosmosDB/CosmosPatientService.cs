using AutoMapper;
using HospitalManagementSystem.Application.DTO.CosmosDB;
using HospitalManagementSystem.Application.Interfaces.CosmosDB;
using HospitalManagementSystem.Domain.Entities.CosmosDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Services.CosmosDB
{
    public class CosmosPatientService : CosmosIPatientService
    {
        private readonly CosmosIPatientRepository _repo;
        private readonly IMapper _mapper;

        public CosmosPatientService(CosmosIPatientRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<CosmosPatientDTO>> GetAll()
        {
            var patients = await _repo.GetAll();
            return _mapper.Map<List<CosmosPatientDTO>>(patients);
        }

        public async Task<CosmosPatientDTO?> GetById(string id)
        {
            var patient = await _repo.GetById(id);
            if (patient == null) return null;

            return _mapper.Map<CosmosPatientDTO>(patient);
        }
        public async Task<CosmosPatientDTO> Create(CosmosCreatePatientDTO dto)
        {
            var patient = _mapper.Map<CosmosPatient>(dto);

            var created = await _repo.Create(patient);

            return _mapper.Map<CosmosPatientDTO>(created);
        }

        public async Task<CosmosPatientDTO> UpdateById(CosmosUpdatePatientDTO dto)
        {
            var patient = _mapper.Map<CosmosPatient>(dto);

            var updated = await _repo.UpdateById(patient);

            return _mapper.Map<CosmosPatientDTO>(updated);
        }

        public async Task Delete(string id)
        {
            await _repo.Delete(id);
        }
    }
}
