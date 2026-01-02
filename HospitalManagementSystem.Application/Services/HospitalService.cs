using AutoMapper;
using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _repo;
        private readonly IMapper _mapper;
        public HospitalService(IHospitalRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<HospitalDTO>> GetAll()
        {
            var hospital = await _repo.GetAll();
            return _mapper.Map<List<HospitalDTO>>(hospital);
        }

        public async Task<HospitalDTO> GetById(int id)
        {
            var hospital = await _repo.GetById(id);
            return _mapper.Map<HospitalDTO>(hospital);
        }
        public async Task Create(HospitalDTO hospitalDTO)
        {
            var hospital = _mapper.Map<Hospital>(hospitalDTO);
            await _repo.Create(hospital);
        }

        public async Task Update(int id, HospitalDTO hospitalDTO)
        {
            var hospital = await _repo.GetById(id);
            if (hospital == null) return;
            hospital.Name = hospitalDTO.Name;
            hospital.Location = hospitalDTO.Location;

            await _repo.Update(hospital);
        }
        
        public async Task Delete(int id)
        {
            var hospital = await _repo.GetById(id);
            if (hospital is not null)
                if (hospital.IsDeleted) throw new Exception("Hospital is already deleted.");
            await _repo.Delete(id);
        }   
    }
}
