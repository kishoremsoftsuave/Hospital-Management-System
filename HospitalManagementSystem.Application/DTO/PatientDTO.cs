using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.DTO
{
    public class PatientDTO
    {
        public required string Name { get; set; }
        public int Age { get; set; }
        public int DoctorId { get; set; }
    }
}
