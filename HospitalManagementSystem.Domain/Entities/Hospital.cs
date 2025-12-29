using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Domain.Entities
{
    public class Hospital
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public ICollection<Doctor> Doctors { get; set; }

    }
}
