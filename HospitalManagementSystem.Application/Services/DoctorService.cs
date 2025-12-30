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
            return _mapper.Map<List<DoctorDTO>>(doctor); // We use this instead of normal
            
        }

        public async Task<DoctorDTO> GetById(int id)
        {
            var doctor = await _repo.GetById(id);
            return _mapper.Map<DoctorDTO>(doctor);
        }

        public async Task Create(DoctorDTO doctorDTO)
        {
            var doctor = _mapper.Map<Doctor>(doctorDTO);
            await _repo.Create(doctor);
        }

        public async Task Update(int id, DoctorDTO doctorDTO)
        {
            var doctor = await _repo.GetById(id);
            if (doctor is null) return;
            _mapper.Map(doctorDTO, doctor);
            await _repo.Update(doctor);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }
    }
}
