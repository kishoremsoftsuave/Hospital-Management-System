using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Infrastructure.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalDB _dbContext;
        public DoctorRepository(HospitalDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Doctor>> GetAll()
        {
            return await _dbContext.Doctors.ToListAsync();
        }
        public async Task<Doctor?> GetById(int id)
        {
            return await (_dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task Create(Doctor doctor)
        {
            _dbContext.Doctors.Add(doctor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Doctor doctor)
        {
            _dbContext.Doctors.Update(doctor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var doctor = await _dbContext.Doctors.IgnoreQueryFilters().FirstOrDefaultAsync(d => d.Id == id);
            if (doctor is null) return;
            doctor.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
