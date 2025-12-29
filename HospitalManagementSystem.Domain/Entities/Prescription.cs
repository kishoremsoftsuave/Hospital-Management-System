using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Domain.Entities
{
    public class Prescription
    {
        public int Id { get; set; }
        public required int DoctorId { get; set; }
        public required int PatientId { get; set; }
        public required DateOnly IssuedDate { get; set; }

    }
}
