using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Domain.Entities
{
    public class Prescription
    {
        public int Id { get; set; }
        public DateOnly IssuedDate { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;


    }
}
