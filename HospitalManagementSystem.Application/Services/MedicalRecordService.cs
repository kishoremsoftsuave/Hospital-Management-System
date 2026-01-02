using AutoMapper;
using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Services
{
    public class MedicalRecordService : IMedicalRecordService
    {
        private readonly IMedicalRecordRepository _repo;
        private readonly IMapper _mapper;
        public MedicalRecordService(IMedicalRecordRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<List<MedicalRecordDTO>> GetAll()
        {
            var medicalRecord = await _repo.GetAll();
            return _mapper.Map<List<MedicalRecordDTO>>(medicalRecord);
        }

        public async Task<MedicalRecordDTO> GetById(int id)
        {
            var medicalRecord = await _repo.GetById(id);
            return _mapper.Map<MedicalRecordDTO>(medicalRecord);
        }

        public async Task Create(MedicalRecordDTO medicalRecordDTO)
        {
            var medicalRecord = _mapper.Map<MedicalRecord>(medicalRecordDTO);
            await _repo.Create(medicalRecord);
        }

        public async Task Update(int id, MedicalRecordDTO medicalRecordDTO)
        {
            var medicalRecord = await _repo.GetById(id);
            if (medicalRecord == null) return;
            _mapper.Map(medicalRecordDTO, medicalRecord);
            await _repo.Update(medicalRecord);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }
    }
}
