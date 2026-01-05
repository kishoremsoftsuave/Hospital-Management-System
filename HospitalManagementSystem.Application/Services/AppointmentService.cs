using AutoMapper;
using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain;
using HospitalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        private readonly IMapper _mapper;
        public AppointmentService(IAppointmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<List<AppointmentDTO>> GetAll()
        {
            var appointment = await _repo.GetAll();
            return _mapper.Map<List<AppointmentDTO>>(appointment);
        }

        public async Task<AppointmentDTO> GetById(int id)
        {
            var appointment = await _repo.GetById(id);
            return _mapper.Map<AppointmentDTO>(appointment);
        }

        public async Task Create(AppointmentDTO appointmentDTO)
        {
            var appointment = _mapper.Map<Appointment>(appointmentDTO);
            await _repo.Create(appointment);
        }

        public async Task Update(int id, AppointmentDTO appointmentDTO)
        {
            var appointment = await _repo.GetById(id);
            if (appointment == null) return;
            _mapper.Map(appointmentDTO, appointment);
            await _repo.Update(appointment);
        }

        public async Task Patch(int id, AppointmentStatusDTO appointmentDTO)
        {
            var appointment = await _repo.GetById(id);
            if (appointment == null) return;
            appointment.Status = appointmentDTO.Status;
            await _repo.Patch(appointment);
        }
        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }
    }
}
