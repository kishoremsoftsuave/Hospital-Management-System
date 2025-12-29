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
        private readonly DoctorDB _dbContext;
        public DoctorRepository(DoctorDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Doctor>> GetAll()
        {
            return await _dbContext.Doctors.ToListAsync();
        }

        public Task Create(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Doctor> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Doctor doctor)
        {
            throw new NotImplementedException();
        }
    }
}
