using AutoMapper;
using System.Threading.Tasks;
using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using HospitalManagementSystem.Domain.Entities;

namespace HospitalManagementSystem.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        private readonly IHospitalRepository _hospitalRepo;
        private readonly IMapper _mapper;
        public DoctorService(IDoctorRepository repo, IMapper mapper, IHospitalRepository hospitalRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _hospitalRepo = hospitalRepo;
        }

        public async Task<List<DoctorDetailDTO>> GetAll()
        {
            var doctor = await _repo.GetAll();
            return _mapper.Map<List<DoctorDetailDTO>>(doctor); 
            
        }

        public async Task<DoctorDetailDTO> GetById(int id)
        {
            var doctor = await _repo.GetById(id);
            return _mapper.Map<DoctorDetailDTO>(doctor);
        }

        public async Task Create(DoctorDTO doctorDTO)
        {
            var hospital = await _hospitalRepo.GetById(doctorDTO.HospitalId);
            if (hospital == null)
                throw new Exception("Hospital not found");
            var doctor = _mapper.Map<Doctor>(doctorDTO);
            await _repo.Create(doctor);
        }

        public async Task Update(int id, DoctorDTO doctorDTO)
        {
            var doctor = await _repo.GetById(id);
            if (doctor is null) return;
            var hospital = await _hospitalRepo.GetById(doctorDTO.HospitalId);
            if (hospital == null)
                throw new Exception("Hospital not found");
            _mapper.Map(doctorDTO, doctor);
            await _repo.Update(doctor);
        }

        public async Task Delete(int id)
        {
            var doctor = await _repo.GetById(id);
            if (doctor is not null)
                if (doctor.IsDeleted) throw new Exception("Doctor is already deleted.");
            await _repo.Delete(id);
        }
    }
}
