using AutoMapper;
using System.Threading.Tasks;
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
        private readonly IMapper _mapper;
        public DoctorService(IDoctorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<DoctorDTO>> GetAll()
        {
            //return (await _repo.GetAll()).Select(d => new DoctorDTO
            //{
            //    Name = d.Name,
            //    Specialization = d.Specialization,
            //    HospitalId = d.HospitalId
            //}).ToList();
            var doctor = await _repo.GetAll();
            return _mapper.Map<List<DoctorDTO>>(doctor);
            
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
