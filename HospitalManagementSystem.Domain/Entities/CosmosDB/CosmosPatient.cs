using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Domain.Entities.CosmosDB
{
    public class CosmosPatient
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");// remove hyphens for Cosmos DB id
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
