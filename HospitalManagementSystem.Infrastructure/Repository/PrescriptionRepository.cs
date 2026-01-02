using HospitalManagementSystem.Application.Interfaces;
using HospitalManagementSystem.Domain.Entities;
using HospitalManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Infrastructure.Repository
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly HospitalDB _dbContext;
        public PrescriptionRepository(HospitalDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Prescription>> GetAll()
        {
            return await _dbContext.prescriptions.ToListAsync();
        }

        public async Task<Prescription?> GetById(int id)
        {
            return await _dbContext.prescriptions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Create(Prescription prescription)
        {
            _dbContext.prescriptions.Add(prescription);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Prescription prescription)
        {
            _dbContext.prescriptions.Add(prescription);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var patient = await _dbContext.prescriptions.FindAsync(id);
            if (patient != null)
                _dbContext.prescriptions.Remove(patient);
            await _dbContext.SaveChangesAsync();
        }
    }
}
