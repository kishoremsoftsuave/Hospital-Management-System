using HospitalManagementSystem.Application.Interfaces.WebAPI;
using HospitalManagementSystem.Domain.Entities.WebAPI;
using HospitalManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Infrastructure.Repository.WebAPI
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalDB _dbContext;
        public PatientRepository(HospitalDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Patient>> GetAll()
        {
            return await _dbContext.Patients.ToListAsync();   
        }

        public async Task<Patient?> GetById(int id)
        {
            return await _dbContext.Patients.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Create(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(d => d.Id == id);
            if (patient is null) return;
            patient.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
