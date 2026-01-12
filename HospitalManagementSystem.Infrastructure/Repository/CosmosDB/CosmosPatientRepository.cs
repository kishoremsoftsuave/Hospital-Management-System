using HospitalManagementSystem.Application.Interfaces.CosmosDB;
using HospitalManagementSystem.Domain.Entities.CosmosDB;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HospitalManagementSystem.Infrastructure.Repository.CosmosDB
{
    public class CosmosPatientRepository : CosmosIPatientRepository
    {
        private readonly Container _container;

        public CosmosPatientRepository(Container container)
        {
            _container = container;
        }

        // GET ALL
        public async Task<List<CosmosPatient>> GetAll()
        {
            var query = _container.GetItemQueryIterator<CosmosPatient>(new QueryDefinition("SELECT * FROM c"));

            var results = new List<CosmosPatient>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.Resource);
            }

            return results;
        }

        // GET BY ID
        public async Task<CosmosPatient?> GetById(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<CosmosPatient>(id, new PartitionKey(id));

                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        // CREATE
        public async Task<CosmosPatient> Create(CosmosPatient patient)
        {
            var response = await _container.CreateItemAsync(patient, new PartitionKey(patient.Id));

            return response.Resource;
        }

        // UPDATE (UPSERT)
        public async Task<CosmosPatient> UpdateById(CosmosPatient patient)
        {
            var response = await _container.ReplaceItemAsync( patient,patient.Id, new PartitionKey(patient.Id));

            return response.Resource;
        }

        // DELETE
        public async Task Delete(string id)
        {
            await _container.DeleteItemAsync<CosmosPatient>( id, new PartitionKey(id));
        }
    }
}
