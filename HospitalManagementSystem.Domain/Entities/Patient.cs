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
        public int DoctorId { get; set; }
    }
}
