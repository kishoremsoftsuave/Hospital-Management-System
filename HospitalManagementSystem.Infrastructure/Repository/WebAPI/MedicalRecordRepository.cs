using HospitalManagementSystem.Application.Interfaces.WebAPI;
using HospitalManagementSystem.Domain.Entities.WebAPI;
using HospitalManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Infrastructure.Repository.WebAPI
{
    public class MedicalRecordsRepository : IMedicalRecordRepository
    {
        private readonly HospitalDB _dbContext;
        public MedicalRecordsRepository(HospitalDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MedicalRecord>> GetAll()
        {
            return await _dbContext.MedicalRecords.ToListAsync();
        }
        public async Task<MedicalRecord?> GetById(int id)
        {
            return await (_dbContext.MedicalRecords.FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task Create(MedicalRecord medicalRecord)
        {
            _dbContext.MedicalRecords.Add(medicalRecord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(MedicalRecord medicalRecord)
        {
            _dbContext.MedicalRecords.Update(medicalRecord);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var medicalRecord = await _dbContext.MedicalRecords.FirstOrDefaultAsync(d => d.Id == id);
            if (medicalRecord is null) return;
            medicalRecord.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
