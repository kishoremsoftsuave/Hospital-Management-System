using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _repo;
        public DoctorService(IDoctorRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<DoctorDTO>> GetAll()
        {
            return (await _repo.GetAll()).Select(d => new DoctorDTO
            {
                Name = d.Name,
                Specialization = d.Specialization,
                HospitalId = d.HospitalId
            }).ToList();
        }
        public Task Create(DoctorDTO doctorDTO)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DoctorDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, DoctorDTO doctorDTO)
        {
            throw new NotImplementedException();
        }
    }
}
