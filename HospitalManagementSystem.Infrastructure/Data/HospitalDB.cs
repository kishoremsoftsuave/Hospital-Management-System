using HospitalManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Infrastructure.Data
{
    public class HospitalDB : DbContext
    {
        public HospitalDB(DbContextOptions<HospitalDB> options) : base(options) { }

        public DbSet<Hospital> Hospitals {get; set;}
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients {get; set;}
    }
}
