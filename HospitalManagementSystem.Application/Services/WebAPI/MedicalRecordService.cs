using AutoMapper;
using HospitalManagementSystem.Application.DTO.WebAPI;
using HospitalManagementSystem.Application.Interfaces.WebAPI;
using HospitalManagementSystem.Domain.Entities.WebAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Services.WebAPI
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _repo;
        private readonly IPatientRepository _patientRepo;
        private readonly IMapper _mapper;
        public MedicalRecordService(IMedicalRecordRepository repo, IMapper mapper, IPatientRepository patientRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _patientRepo = patientRepo;
        }
        public async Task<List<MedicalRecordDTO>> GetAll()
        {
            var medicalRecord = await _repo.GetAll();
            return _mapper.Map<List<MedicalRecordDTO>>(medicalRecord);
        }

        public async Task<MedicalRecordDTO> GetById(int id)
        {
            var medicalRecord = await _repo.GetById(id);
            if (medicalRecord is null)
                throw new KeyNotFoundException("MedicalRecord not found");
            return _mapper.Map<MedicalRecordDTO>(medicalRecord);
        }

        public async Task Create(MedicalRecordDTO medicalRecordDTO)
        {
            var patientExists = await _patientRepo.GetById(medicalRecordDTO.PatientId);
            if (patientExists is null)
                throw new KeyNotFoundException("Patient does not exist.");
            var medicalRecord = _mapper.Map<MedicalRecord>(medicalRecordDTO);
            await _repo.Create(medicalRecord);
        }

        public async Task Update(int id, MedicalRecordDTO medicalRecordDTO)
        {
            var medicalRecord = await _repo.GetById(id);
            if (medicalRecord is null)
                throw new KeyNotFoundException("MedicalRecord not found");
            var patientExists = await _patientRepo.GetById(medicalRecordDTO.PatientId);
            if (patientExists is null)
                throw new KeyNotFoundException("Patient does not exist.");
            _mapper.Map(medicalRecordDTO, medicalRecord);
            await _repo.Update(medicalRecord);
        }

        public async Task Delete(int id)
        {
            var medical = await _repo.GetById(id);
            if (medical is null)
                throw new KeyNotFoundException("MedicalRecord not found");
            if (medical.IsDeleted)
                throw new InvalidOperationException("MedicalRecord is already deleted.");
            await _repo.Delete(id);
        }
    }
}
