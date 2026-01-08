using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem.Infrastructure.Injection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<CosmosClient>(_ =>
        {
            var endpoint = configuration["CosmosDb:AccountEndpoint"];
            var key = configuration["CosmosDb:AccountKey"];

            var options = new CosmosClientOptions
            {
                HttpClientFactory = () =>
                {
                    var handler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback =
                            (req, cert, chain, errors) => true
                    };
                    return new HttpClient(handler);
                }
            };

            return new CosmosClient(endpoint, key, options);
        });

        return services;
    }
}
