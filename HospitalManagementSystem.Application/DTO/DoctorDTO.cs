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
    public class DoctorDetailDTO
    {
        public string DoctorName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string HospitalName { get; set; } = string.Empty;
        public string HospitalAddress { get; set; } = string.Empty;
    }
    public class  HospitalDoctorDTO
    {
        public string DoctorName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
    }
}
