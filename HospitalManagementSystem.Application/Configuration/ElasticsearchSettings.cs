using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Configuratioon
{
    public class ElasticsearchSettings
    {
        public string Uri { get; set; } = "https://localhost:9200";
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
