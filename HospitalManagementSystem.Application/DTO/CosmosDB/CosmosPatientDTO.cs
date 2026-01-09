using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.DTO.CosmosDB
{
    public class CosmosCreatePatientDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Disease { get; set; } = string.Empty;
    }

    // DTO used for returning patient data
    public class CosmosPatientDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Disease { get; set; } = string.Empty;
    }

    // DTO used for updating a patient
    public class CosmosUpdatePatientDTO
    {
        public string Id { get; set; } = string.Empty; // Must match existing patient
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Disease { get; set; } = string.Empty;
    }
}
