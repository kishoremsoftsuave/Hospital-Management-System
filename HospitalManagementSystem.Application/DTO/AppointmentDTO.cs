using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.DTO
{
    public class AppointmentDTO
    {
        public required DateOnly AppointmentDate { get; set; }
        public required int PatientId { get; set; }
        public required int DoctorId { get; set; }
    }
}
