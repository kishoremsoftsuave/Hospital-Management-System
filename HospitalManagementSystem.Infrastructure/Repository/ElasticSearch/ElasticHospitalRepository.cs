using Azure;
using Elastic.Clients.Elasticsearch;
using HospitalManagementSystem.Application.DTO.ElasticSearch;
using HospitalManagementSystem.Application.Interfaces.ElasticSearch;
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
        
        public async Task<IEnumerable<ElasticHospitalDetailDTO>> GetAll()
        {
            var response = await _client.SearchAsync<ElasticHospitalDetailDTO>(s => s.Indices(IndexName).Size(1000));
            return response.Documents;
        }

        public async Task<ElasticHospitalDetailDTO?> GetById(Guid id)
        {
            var response = await _client.GetAsync<ElasticHospitalDetailDTO>(id.ToString(), g => g.Index(IndexName));
            //var response = await _client.SearchAsync<ElasticHospitalDTO>(h => h.Indices(IndexName).Query(q => q.Term(t => t.Field(f => f.Hospital.Id).Value(id))));
            //we did not use above search query because it is less efficient than GetAsync method because GetAsync method directly retrieves the document by its ID but search query has to search through the index to find the document.
            return response.Found ? response.Source : null;
        }

        public async Task Create(ElasticHospitalCreateDTO hospitalDTO)
        {
            await _client.IndexAsync(hospitalDTO, i => i.Index(IndexName));
        }

        public async Task Update(Guid id, ElasticHospitalDetailDTO hospitalDTO)
        {
            await _client.IndexAsync(hospitalDTO, i => i.Index(IndexName).Id(id.ToString()));
        }

        public async Task Delete(Guid id)
        {
            await _client.DeleteAsync<ElasticHospitalDetailDTO>(id.ToString(), d => d.Index(IndexName));
        }
    }
}
