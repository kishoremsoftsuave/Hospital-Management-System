using HospitalManagementSystem.Domain.Entities.WebAPI;
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
        public DbSet<Appointment> Appointments {get; set;}
        public DbSet<MedicalRecord> MedicalRecords {get; set;}
        public DbSet<Prescription> Prescriptions {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Hospital)
                .WithMany(h => h.Doctors)
                .HasForeignKey(d => d.HospitalId);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.Patients)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany()
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .HasConversion<string>();


            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(p => p.DoctorId);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Patient)
                .WithMany(p => p.MedicalRecords)
                .HasForeignKey(m => m.PatientId);

            modelBuilder.Entity<Doctor>()
                .HasQueryFilter(d => !d.IsDeleted)
                .Property(d => d.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<Patient>()
                .HasQueryFilter(p => !p.IsDeleted)
                .Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<Appointment>()
                .HasQueryFilter(a => !a.IsDeleted)
                .Property(a => a.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<Prescription>()
                .HasQueryFilter(p => !p.IsDeleted)
                .Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<MedicalRecord>()
                .HasQueryFilter(m => !m.IsDeleted)
                .Property(m => m.IsDeleted)
                .HasDefaultValue(false);
        }

    }
}
