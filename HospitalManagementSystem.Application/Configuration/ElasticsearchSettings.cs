using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Application.Configuratioon
{
    public class ElasticsearchSettings
    {
        public string Uri { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
