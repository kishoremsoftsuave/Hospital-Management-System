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

        public async Task<List<HospitalDetailDTO>> GetAll()
        {
            var hospital = await _repo.GetAll();
            return _mapper.Map<List<HospitalDetailDTO>>(hospital);
        }

        public async Task<HospitalDetailDTO> GetById(int id)
        {
            var hospital = await _repo.GetById(id);
            if (hospital is null)
                throw new KeyNotFoundException("Hospital not found");
            return _mapper.Map<HospitalDetailDTO>(hospital);
        }
        public async Task Create(HospitalDTO hospitalDTO)
        {
            var hospital = _mapper.Map<Hospital>(hospitalDTO);
            await _repo.Create(hospital);
        }

        public async Task Update(int id, HospitalDTO hospitalDTO)
        {
            var hospital = await _repo.GetById(id);
            if (hospital == null)
                throw new KeyNotFoundException("Hospital not found");
            _mapper.Map(hospitalDTO, hospital);
            await _repo.Update(hospital);
        }
        
        public async Task Delete(int id)
        {
            var hospital = await _repo.GetById(id);
            if (hospital is null)
                throw new KeyNotFoundException("Hospital not found");
            if (hospital.IsDeleted) 
                throw new InvalidOperationException("Hospital is already deleted.");
            await _repo.Delete(id);
        }   
    }
}
