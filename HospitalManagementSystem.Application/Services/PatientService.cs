using AutoMapper;
using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repo;
        private readonly IMapper _mapper;
        public PatientService (IPatientRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<List<PatientDTO>> GetAll()
        {
            var patient = await _repo.GetAll();
            return _mapper.Map<List<PatientDTO>>(patient);
        }

        public async Task<PatientDTO> GetById(int id)
        {
            var patient = await _repo.GetById(id);
            return _mapper.Map<PatientDTO>(patient);
        }

        public async Task Create(PatientDTO patientDTO)
        {
            var patient = _mapper.Map<Patient>(patientDTO);
            await _repo.Create(patient);
        }

        public async Task Update(int id, PatientDTO patientDTO)
        {
            var patient = await _repo.GetById(id);
            if (patient == null) return;
            _mapper.Map(patientDTO, patient);
            await _repo.Update(patient);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }
    }
}
