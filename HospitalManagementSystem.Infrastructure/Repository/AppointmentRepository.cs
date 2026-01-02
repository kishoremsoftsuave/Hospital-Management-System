using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Infrastructure.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {

        private readonly HospitalDB _dbContext;
        public AppointmentRepository(HospitalDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Appointment>> GetAll()
        {
            return await _dbContext.appointments.ToListAsync();
        }
        public async Task<Appointment?> GetById(int id)
        {
            return await (_dbContext.appointments.FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task Create(Appointment appointment)
        {
            _dbContext.appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Appointment appointment)
        {
            _dbContext.appointments.Update(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var appointment = await _dbContext.Hospitals.FirstOrDefaultAsync(d => d.Id == id);
            if (appointment is null) return;
            appointment.IsDeleted = true;
            _dbContext.Hospitals.Update(appointment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
