using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<Hospital?> GetById(int id)
        {
            return await _dbContext.Hospitals.FirstOrDefaultAsync(h => h.Id == id);
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
            var hospital = await _dbContext.Hospitals.FirstOrDefaultAsync(d => d.Id == id);
            if (hospital is null) return;
            hospital.IsDeleted = true;
            _dbContext.Hospitals.Update(hospital);
            await _dbContext.SaveChangesAsync();
        }
    }
}
