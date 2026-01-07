using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;
using HospitalManagementSystem.Domain.Entities.ElasticSearch;
using Microsoft.Extensions.Logging;

namespace HospitalManagementSystem.Infrastructure.ElasticSearch
{
    public class ElasticDB
    {
        private readonly ElasticsearchClient _client;
        private readonly ILogger<ElasticDB> _logger;
        private const string IndexName = "hospitals";

        public ElasticDB(ElasticsearchClient client , ILogger<ElasticDB> logger)
        {
            _client = client;
            _logger = logger;
        }
        public async Task<bool> CreateAllIndicesAsync()
        {
            try
            {
                var hospital = await CreateHospitalIndexAsync();
                return hospital;
            }
            catch (Exception ex)
            { 
            _logger.LogError(ex, "Error creating indices");
            return false;
            }   
        }
        public async Task<bool> CreateHospitalIndexAsync()
        {
            var existsResponse = await _client.Indices.ExistsAsync(IndexName);
            if (existsResponse.Exists)
            {
                _logger.LogInformation("Hospital index already exists");
                return true;
            }
            var response = await _client.Indices.CreateAsync(IndexName, c => c
                .Settings(s => s
                    .NumberOfShards(1)
                    .NumberOfReplicas(0)
                    .Analysis(a => a
                        .Analyzers(an => an
                            .Custom("hospital_analyzer", ca => ca
                                .Tokenizer("standard")
                                .Filter("lowercase", "stop")
                            )
                        )
                    )
                )
                .Mappings(m => m
                //   .Properties(ps => ps
                //        //.IntegerNumber(n => n.Name(nameof(ElasticHospital.Id)))
                //        .Text(t => t.Name(nameof(ElasticHospital.Name)))
                //        .Text(t => t.Name(nameof(ElasticHospital.Location)))
                //    )
                //)
                .Properties(new Properties
                    {
                        {
                            nameof(ElasticHospital.Name),
                            new TextProperty
                            {
                                Analyzer = "hospital_analyzer"
                            }
                        } ,

                        {
                            nameof(ElasticHospital.Location),
                            new TextProperty
                            {
                                Analyzer = "hospital_analyzer"
                            }
                        }
                    } )
                )
            );

            return response.IsValidResponse;
        }

        public async Task<bool> DeleteAllIndiciesAsync()
        {
            try
            {
                var existsResponse = await _client.Indices.ExistsAsync(IndexName);
                if (!existsResponse.Exists)
                {
                    _logger.LogInformation(" Index {Index} does not exist", IndexName);
                    return true;
                }
                var response = await _client.Indices.DeleteAsync(IndexName);
                if (response.IsValidResponse)
                {
                    _logger.LogInformation(" Index {Index} deleted successfully", IndexName);
                    return true;
                }
                else
                {
                     _logger.LogError(" Failed to delete index {Index}: {Error}", IndexName, response.DebugInformation);
                     return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting index {Index}", IndexName);
                return false;
            }
        }
        public async Task<bool> DeleteHospitalAsync(int id)
        {
            try
            {
                var response = await _client.DeleteAsync<ElasticHospital>(id.ToString(), d => d.Index(IndexName));

                if (response.IsValidResponse)
                {
                    _logger.LogInformation("Hospital document with ID {Id} deleted successfully", id);
                    return true;
                }

                _logger.LogError("Failed to delete hospital document with ID {Id}: {Error}", id, response.DebugInformation);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting hospital document with ID {Id}", id);
                return false;
            }
        }
    }
}
