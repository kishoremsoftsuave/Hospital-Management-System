using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Domain.Entities
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public required int PatientId { get; set; }
        public required string Diagnosis { get; set; }
    }
}
