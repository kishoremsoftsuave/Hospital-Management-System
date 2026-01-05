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
            return await _dbContext.Appointments.ToListAsync();
        }
        public async Task<Appointment?> GetById(int id)
        {
            return await (_dbContext.Appointments.FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task Create(Appointment appointment)
        {
            _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Appointment appointment)
        {
            _dbContext.Appointments.Update(appointment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Patch(Appointment appointment)
        {
            _dbContext.Appointments.Update(appointment);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var appointment = await _dbContext.Appointments.FirstOrDefaultAsync(d => d.Id == id);
            if (appointment is null) return;
            appointment.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
