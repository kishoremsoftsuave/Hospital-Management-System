using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.DTO.WebAPI
{
    public class HospitalDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
    public class HospitalDetailDTO
    {
        public string HospitalName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public List<HospitalDoctorDTO> Doctors { get; set; } = new List<HospitalDoctorDTO>();
    }
}
