using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Infrastructure.Injection
{
    public static class CosmosInitializer
    {
        public static async Task InitializeAsync(CosmosClient client, string databaseId, string containerId, string partitionKeyPath) 
        {
            var db = await client.CreateDatabaseIfNotExistsAsync(databaseId); 
            await db.Database.CreateContainerIfNotExistsAsync(id: containerId, partitionKeyPath: partitionKeyPath, throughput: 400);
        }
    }
}
