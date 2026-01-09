using AutoMapper;
using HospitalManagementSystem.Application.DTO.WebAPI;
using HospitalManagementSystem.Application.Interfaces.WebAPI;
using HospitalManagementSystem.Domain.Entities.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Services.WebAPI
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repo;
        private readonly IDoctorRepository _doctorRepo;

        private readonly IMapper _mapper;
        public PatientService (IPatientRepository repo, IMapper mapper, IDoctorRepository doctorRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _doctorRepo = doctorRepository;
        }
        public async Task<List<PatientDTO>> GetAll()
        {
            var patient = await _repo.GetAll();
            return _mapper.Map<List<PatientDTO>>(patient);
        }

        public async Task<PatientDTO> GetById(int id)
        {
            var patient = await _repo.GetById(id);
            if (patient is null)
                throw new KeyNotFoundException("Patient not found.");
            return _mapper.Map<PatientDTO>(patient);
        }

        public async Task Create(PatientDTO patientDTO)
        {
            var doctor = await _doctorRepo.GetById(patientDTO.DoctorId);
            if (doctor is null) 
                throw new KeyNotFoundException("Doctor not found.");
            var patient = _mapper.Map<Patient>(patientDTO);
            await _repo.Create(patient);
        }

        public async Task Update(int id, PatientDTO patientDTO)
        {
            var patient = await _repo.GetById(id);
            if (patient is null)
                throw new KeyNotFoundException("Patient not found.");
            var doctor = await _doctorRepo.GetById(patientDTO.DoctorId);
            if (doctor is null)
                throw new KeyNotFoundException("Doctor not found.");
            _mapper.Map(patientDTO, patient);
            await _repo.Update(patient);
        }

        public async Task Delete(int id)
        {
            var patient = await _repo.GetById(id);
            if (patient is null)
                throw new KeyNotFoundException("Patient not found.");
            if (patient.IsDeleted)
               throw new InvalidOperationException("Patient is already deleted.");
            await _repo.Delete(id);
        }
    }
}
