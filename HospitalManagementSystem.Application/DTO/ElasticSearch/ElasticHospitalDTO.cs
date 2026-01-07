using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.DTO.ElasticSearch
{
    public class ElasticHospitalCreateDTO
    {
        public required Guid id { get; set; }
        public required string Name { get; set; } 
        public required string Location { get; set; }
    }

    public class ElasticHospitalUpdateDTO
    {
        public required string Name { get; set; }
        public required string Location { get; set; }
    }

    public class ElasticHospitalDetailDTO
    {
        public string HospitalName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        //public List<HospitalDoctorDTO> Doctors { get; set; } = new List<HospitalDoctorDTO>();
    }
}
