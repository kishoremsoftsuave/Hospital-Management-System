using Azure;
using Elastic.Clients.Elasticsearch;
using HospitalManagementSystem.Application.DTO.ElasticSearch;
using HospitalManagementSystem.Application.Interfaces.ElasticSearch;
using HospitalManagementSystem.Infrastructure.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Infrastructure.Repository.ElasticSearch
{
    public class ElasticHospitalRepository : IElasticHospitalRepository
    {
        private readonly ElasticsearchClient _client;
        private readonly ElasticDB _db;
        private const string IndexName = "hospitals";
        public ElasticHospitalRepository(ElasticsearchClient client , ElasticDB dB)
        {
            _client = client;
            _db = dB;
        }
        
        public async Task<IEnumerable<ElasticHospitalDTO>> GetAll()
        {
            var response = await _client.SearchAsync<ElasticHospitalDTO>(s => s.Indices(IndexName).Size(1000));
            return response.Documents;
        }

        public async Task<ElasticHospitalDTO?> GetById(int id)
        {
            var response = await _client.GetAsync<ElasticHospitalDTO>(id.ToString(), g => g.Index(IndexName));
            //var response = await _client.SearchAsync<ElasticHospitalDTO>(h => h.Indices(IndexName).Query(q => q.Term(t => t.Field(f => f.Hospital.Id).Value(id))));
            //we did not use above search query because it is less efficient than GetAsync method because GetAsync method directly retrieves the document by its ID but search query has to search through the index to find the document.
            return response.Found ? response.Source : null;
        }

        public async Task Create(ElasticHospitalDTO hospitalDTO)
        {
            await _client.IndexAsync(hospitalDTO, i => i.Index(IndexName));
        }

        public async Task Update(int id, ElasticHospitalDTO hospitalDTO)
        {
            await _client.IndexAsync(hospitalDTO, i => i.Index(IndexName).Id(id.ToString()));
        }

        public async Task Delete(int id)
        {
            await _client.DeleteAsync<ElasticHospitalDTO>(id.ToString(), d => d.Index(IndexName));
        }
    }
}
