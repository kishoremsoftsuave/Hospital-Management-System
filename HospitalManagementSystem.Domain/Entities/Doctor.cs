using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Domain.Entities
{
    public class    Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; } = null!;
        public ICollection<Patient> Patients { get; set; } = new List<Patient>();
        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
        public bool IsDeleted { get; set; } = false;
    }
}
