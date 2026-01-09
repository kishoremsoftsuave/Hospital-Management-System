using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Infrastructure.Injection
{
    public class CosmosDbOptions
    {
        public string AccountEndpoint { get; set; } = null!;
        public string AccountKey { get; set; } = null!;
    }
}
