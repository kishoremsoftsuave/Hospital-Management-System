using Elastic.Clients.Elasticsearch;
using HospitalManagementSystem.Application.DTO;
using HospitalManagementSystem.Application.DTO.ElasticSearch;
using HospitalManagementSystem.Application.Interfaces.ElasticSearch;
using HospitalManagementSystem.Domain.Entities.ElasticSearch;

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

        public async Task<IEnumerable<ElasticHospital>> GetAll()
        {
            var response = await _client.SearchAsync<ElasticHospital>(s => s
                .Indices(IndexName)
                .Size(1000)
            );
            return response.Documents;
        }

        public async Task<ElasticHospital?> GetById(Guid id)
        {
            var response = await _client.GetAsync<ElasticHospital>(id.ToString(), g => g.Index(IndexName));
            return response.Found ? response.Source : null;
        }

        public async Task Create(ElasticHospital hospital)
        {
            await _client.IndexAsync(hospital, i => i
                .Index(IndexName)
                .Id(hospital.Id.ToString())
            );
        }

        public async Task Update(Guid id, ElasticHospital hospital)
        {
            //await _client.UpdateAsync<ElasticHospital, ElasticHospital>(IndexName, id.ToString(), u => u.Doc(hospital));
            //await _client.UpdateAsync<ElasticHospital, ElasticHospital>(u => u
            //    .Index(IndexName)   // specify the index
            //    .Id(id)             // document ID
            //    .Doc(hospital)      // partial update object
            //);

            var updateRequest = new UpdateRequest<ElasticHospital, ElasticHospital>(IndexName, id)
            {
                Doc = hospital
            };

            await _client.UpdateAsync(updateRequest);

        }

        public async Task Delete(Guid id)
        {
            await _client.DeleteAsync(IndexName, id.ToString());
        }
    }
}
