using HospitalManagementSystem.Application.Interfaces.WebAPI;
using HospitalManagementSystem.Domain.Entities.WebAPI;
using HospitalManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Infrastructure.Repository.WebAPI
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
            return await _dbContext.Hospitals.Include(h => h.Doctors).ToListAsync();
        }

        public async Task<Hospital?> GetById(int id)
        {
            return await _dbContext.Hospitals.Include(h => h.Doctors).FirstOrDefaultAsync(h => h.Id == id);
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
            await _dbContext.SaveChangesAsync();
        }
    }
}
