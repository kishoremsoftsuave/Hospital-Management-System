using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces.WebAPI;
using HospitalManagementSystem.Domain.Entities.WebAPI;
using HospitalManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Infrastructure.Repository.WebAPI
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
            return await _dbContext.Doctors.Include(d => d.Hospital).ToListAsync();
        }
        public async Task<Doctor?> GetById(int id)
        {
            return await (_dbContext.Doctors.Include(d => d.Hospital).FirstOrDefaultAsync(x => x.Id == id));
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
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (doctor is null) return;
            doctor.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
