using Elastic.Clients.Elasticsearch;
using HospitalManagementSystem.Application.DTO.ElasticSearch;
using HospitalManagementSystem.Application.Interfaces.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagementSystem.Infrastructure.Repository.ElasticSearch
{
    public class ElasticHospitalRepository : IElasticHospitalRepository
    {
        private readonly ElasticsearchClient _client;
        private const string IndexName = "hospitals";
        public ElasticHospitalRepository(ElasticsearchClient client)
        {
            _client = client;
        }
        
        public async Task<IEnumerable<ElasticHospitalDTO>> GetAll()
        {
            var response = await _client.SearchAsync<ElasticHospitalDTO>(s => s.Indices(IndexName).Size(1000));
            return response.Documents;
        }

        public async Task<ElasticHospitalDTO?> GetById(int id)
        {
            var response = await _client.GetAsync<ElasticHospitalDTO>(id.ToString(), g => g.Index(IndexName));
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
