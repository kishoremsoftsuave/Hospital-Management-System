using HospitalManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Infrastructure.Data
{
    public class DoctorDB : DbContext
    {
        public DoctorDB(DbContextOptions<DoctorDB> options) : base(options) { }

        public DbSet<Doctor> Doctors { get; set; }
    }
}
