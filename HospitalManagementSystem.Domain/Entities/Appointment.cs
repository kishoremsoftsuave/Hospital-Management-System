using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Domain.Entities
{
    public class Appointment
    {
        // { "id": "a4001", "patient_id": "p3001", "doctor_id": "d2001", "appointment_date": "2025-01-10", "status": "Confirmed" }

        public int Id { get; set; }
        public required int PatientId {  get; set; }
        public int DoctorId { get; set; }
        public required DateOnly AppointmentDate {  get; set; }
        public string Status { get; set; } = string.Empty;

    }
}
