using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.DTO
{
    public class DoctorDTO
    {
        public required string Name { get; set; }
        public required string Specialization { get; set; }
        public required int HospitalId { get; set; }
    }
}
