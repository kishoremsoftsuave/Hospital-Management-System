using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.DTO
{
    public class MedicalRecordDTO
    {
        public string Diagnosis { get; set; } = string.Empty;
        public int PatientId { get; set; }
    }
}
