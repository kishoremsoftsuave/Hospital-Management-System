//using Microsoft.Azure.Cosmos;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace HospitalManagementSystem.Infrastructure.Injection;

//public static class DependencyInjection
//{
//    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
//    {
//        services.AddSingleton<CosmosClient>(_ =>
//        {
//            var endpoint = configuration["CosmosDb:AccountEndpoint"];
//            var key = configuration["CosmosDb:AccountKey"];

//            var options = new CosmosClientOptions
//            {
//                HttpClientFactory = () =>
//                {
//                    var handler = new HttpClientHandler
//                    {
//                        ServerCertificateCustomValidationCallback =
//                            (req, cert, chain, errors) => true
//                    };
//                    return new HttpClient(handler);
//                }
//            };

//            return new CosmosClient(endpoint, key, options);
//        });

//        return services;
//    }
//}

//using HospitalManagementSystem.Infrastructure.Data;
//using Microsoft.Azure.Cosmos;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Options;

//namespace HospitalManagementSystem.Infrastructure.Injection;

//public static class DependencyInjection
//{
//    public static IServiceCollection AddInfrastructure(
//        this IServiceCollection services,
//        IConfiguration configuration)
//    {
//        // Bind CosmosDbOptions
//        services.AddOptions<CosmosDbOptions>()
//            .Bind(configuration.GetSection("CosmosDb"))
//            .Validate(o =>
//                !string.IsNullOrWhiteSpace(o.AccountEndpoint) &&
//                !string.IsNullOrWhiteSpace(o.AccountKey),
//                "CosmosDb configuration is invalid")
//            .ValidateOnStart();

//        // Register CosmosClient
//        services.AddSingleton<CosmosClient>(sp =>
//        {
//            var options = sp
//                .GetRequiredService<IOptions<CosmosDbOptions>>()
//                .Value;

//            var clientOptions = new CosmosClientOptions();

//            return new CosmosClient(
//                options.AccountEndpoint,
//                options.AccountKey,
//                clientOptions);
//        });
//        services.AddSingleton<CosmosDB>();

//        return services;
//    }
//}
using HospitalManagementSystem.Application.Services;
using HospitalManagementSystem.Infrastructure.Data;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HospitalManagementSystem.Infrastructure.Injection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Bind CosmosDbOptions
            services.AddOptions<CosmosDbOptions>()
                .Bind(configuration.GetSection("CosmosDb"))
                .Validate(o =>
                    !string.IsNullOrWhiteSpace(o.AccountEndpoint) &&
                    !string.IsNullOrWhiteSpace(o.AccountKey),
                    "CosmosDb configuration is invalid")
                .ValidateOnStart();

            // Register CosmosClient
            services.AddSingleton<CosmosClient>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<CosmosDbOptions>>().Value;
                return new CosmosClient(options.AccountEndpoint, options.AccountKey, new CosmosClientOptions());
            });

            // Register concrete CosmosDbService
            services.AddSingleton<CosmosDB>();
            services.AddSingleton(sp =>
            {
                var cosmosDb = sp.GetRequiredService<CosmosDB>();
                var options = configuration.GetSection("CosmosDb");
                var databaseId = options["DatabaseId"];
                var containerId = options["ContainerId"];
                var partitionKey = options["PartitionKeyPath"] ?? "/id";
                return new PatientService(cosmosDb, databaseId, containerId, partitionKey);
            });

            return services;
        }
    }
}
