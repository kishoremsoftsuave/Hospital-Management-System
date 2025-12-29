using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Infrastructure.Repository
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly HospitalDB _dbContext;
        public HospitalRepository (HospitalDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Hospital>> GetAll()
        {
            return await _dbContext.Hospitals.ToListAsync();
        }

        public async Task<Hospital> GetById(int id)
        {
            return await _dbContext.Hospitals.FirstAsync();
        }
        public async Task Create(Hospital hospital)
        {
            _dbContext.Hospitals.Add(hospital);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Hospital hospital)
        {
            _dbContext.Hospitals.Update(hospital);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var hospital = await _dbContext.Hospitals.FindAsync(id);
            if (hospital is null) return;
            _dbContext.Hospitals.Remove(hospital);
            await _dbContext.SaveChangesAsync();
        }
    }
}
