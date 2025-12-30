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
        public HospitalService(IHospitalRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<HospitalDTO>> GetAll()
        {
            return (await _repo.GetAll()).Select(h => new HospitalDTO
            {
                Name = h.Name,
                Location = h.Location
            }).ToList();
        }

        public async Task<HospitalDTO> GetById(int id)
        {   
            var h = await _repo.GetById(id);
            if (h is null)
                throw new Exception("Invalid Id");
            return new HospitalDTO
            {
                Name = h.Name,
                Location = h.Location
            };
        }
        public async Task Create(HospitalDTO hospitalDTO)
        {
            await _repo.Create(new Hospital
            {
                Name = hospitalDTO.Name,
                Location = hospitalDTO.Location
            });
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
            await _repo.Delete(id);
        }   
    }
}
