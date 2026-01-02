using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.DTO
{
    public class PrescriptionDTO
    {
        public DateOnly IssuedDate { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

    }
}
