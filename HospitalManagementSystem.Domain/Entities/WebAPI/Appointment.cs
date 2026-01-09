using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Domain.Entities.WebAPI
{
    public class Appointment
    {
        // { "id": "a4001", "patient_id": "p3001", "doctor_id": "d2001", "appointment_date": "2025-01-10", "status": "Confirmed" }

        public int Id { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public int PatientId {  get; set; }
        public Patient Patient { get; set; } = null!;
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;

    }
}
