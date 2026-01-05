using AutoMapper;
using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _repo;
        private readonly IDoctorRepository _doctorRepo;
        private readonly IPatientRepository _patientRepo;
        private readonly IMapper _mapper;
        public PrescriptionService(IPrescriptionRepository repo, IMapper mapper, IDoctorRepository doctorRepo, IPatientRepository patientRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
        }
        public async Task<List<PrescriptionDTO>> GetAll()
        {
            var prescription = await _repo.GetAll();
            return _mapper.Map<List<PrescriptionDTO>>(prescription);
        }

        public async Task<PrescriptionDTO> GetById(int id)
        {
            var prescription = await _repo.GetById(id);
            return _mapper.Map<PrescriptionDTO>(prescription);
        }

        public async Task Create(PrescriptionDTO prescriptionDTO)
        {
            var doctor = await _doctorRepo.GetById(prescriptionDTO.DoctorId);
            if (doctor == null) throw new Exception("Doctor not found.");
            var patient = await _patientRepo.GetById(prescriptionDTO.PatientId);
            if (patient == null) throw new Exception("Patient not found.");
            var prescription = _mapper.Map<Prescription>(prescriptionDTO);
            await _repo.Create(prescription);
        }

        public async Task Update(int id, PrescriptionDTO prescriptionDTO)
        {
            var prescription = await _repo.GetById(id);
            if (prescription == null) return;
            var doctor = await _doctorRepo.GetById(prescriptionDTO.DoctorId);
            if (doctor == null) throw new Exception("Doctor not found.");
            var patient = await _patientRepo.GetById(prescriptionDTO.PatientId);
            if (patient == null) throw new Exception("Patient not found.");
            _mapper.Map(prescriptionDTO, prescription);
            await _repo.Update(prescription);
        }

        public async Task Delete(int id)
        {
            var prescription = await _repo.GetById(id);
            if (prescription is not null)
                if (prescription.IsDeleted) throw new Exception("Prescription is already deleted.");
            await _repo.Delete(id);
        }
    }
}
