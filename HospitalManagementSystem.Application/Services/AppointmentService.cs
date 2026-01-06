using AutoMapper;
using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain;
using HospitalManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace HospitalManagementSystem.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        private readonly IDoctorRepository _doctorRepo;
        private readonly IPatientRepository _patientRepo;
        private readonly IMapper _mapper;
        public AppointmentService(IAppointmentRepository repo, IMapper mapper, IDoctorRepository doctorRepo, IPatientRepository patientRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
        }
        public async Task<List<AppointmentDTO>> GetAll()
        {
            var appointment = await _repo.GetAll();
            return _mapper.Map<List<AppointmentDTO>>(appointment);
        }

        public async Task<AppointmentDTO> GetById(int id)
        {
            var appointment = await _repo.GetById(id);
            if(appointment == null)
                throw new KeyNotFoundException("Appointment not found");
            return _mapper.Map<AppointmentDTO>(appointment);
        }

        public async Task Create(AppointmentDTO appointmentDTO)
        {
            var doctor = await _doctorRepo.GetById(appointmentDTO.DoctorId);
            if (doctor == null)
                throw new KeyNotFoundException("Doctor not found");

            if (doctor.IsDeleted)
                throw new InvalidOperationException("Doctor is deleted");

            // Validate Patient
            var patient = await _patientRepo.GetById(appointmentDTO.PatientId);
            if (patient == null)
                throw new KeyNotFoundException("Patient not found");

            // Business rule example
            if (appointmentDTO.AppointmentDate < DateOnly.FromDateTime(DateTime.Today))
                throw new ArgumentException("Appointment date cannot be in the past");

            var appointment = _mapper.Map<Appointment>(appointmentDTO);
            await _repo.Create(appointment);
        }

        public async Task Update(int id, AppointmentDTO appointmentDTO)
        {
            var appointment = await _repo.GetById(id);
            if (appointment == null)
                throw new KeyNotFoundException("Appointment not found");

            var doctor = await _doctorRepo.GetById(appointmentDTO.DoctorId);
            if (doctor == null)
                throw new KeyNotFoundException("Doctor not found");

            if (doctor.IsDeleted)
                throw new InvalidOperationException("Doctor is deleted");

            // Validate Patient
            var patient = await _patientRepo.GetById(appointmentDTO.PatientId);
            if (patient == null)
                throw new KeyNotFoundException("Patient not found");

            // Business rule example
            if (appointmentDTO.AppointmentDate < DateOnly.FromDateTime(DateTime.Today))
                throw new ArgumentException("Appointment date cannot be in the past");

            _mapper.Map(appointmentDTO, appointment);
            await _repo.Update(appointment);
        }

        public async Task Patch(int id, AppointmentStatusDTO appointmentDTO)
        {
            var appointment = await _repo.GetById(id);
            if (appointment == null)
                throw new KeyNotFoundException("Appointment not found");
            appointment.Status = appointmentDTO.Status;
            await _repo.Patch(appointment);
        }
        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }
    }
}
