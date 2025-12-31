using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    }
}
