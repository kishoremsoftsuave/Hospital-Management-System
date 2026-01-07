using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Mapping;
using HospitalManagementSystem.Domain.Entities.ElasticSearch;

namespace HospitalManagementSystem.Infrastructure.ElasticSearch
{
    public class ElasticDB
    {
        private readonly ElasticsearchClient _client;
        private const string IndexName = "hospitals";

        public ElasticDB(ElasticsearchClient client)
        {
            _client = client;
        }
        public async Task<bool> CreateAllIndicesAsync()
        {
            try
            {
                var hospital = await CreateHospitalIndexAsync();
                return hospital;
            }
            catch (Exception)
            {
                return false;
            }   
        }
        public async Task<bool> CreateHospitalIndexAsync()
        {
            var existsResponse = await _client.Indices.ExistsAsync(IndexName);
            if (existsResponse.Exists)
                return true;

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

        public async Task<bool> DeleteHospitalIndexAsync()
        {
            var existsResponse = await _client.Indices.ExistsAsync(IndexName);
            if (existsResponse.Exists)
                return true;

            var response = await _client.Indices.DeleteAsync(IndexName);
            return response.IsValidResponse;
        }
    }
}
