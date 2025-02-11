using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RPS.Services.Accounts.Data.MongoDbService;
using RPS.Services.Accounts.Options;

namespace RPS.Services.Accounts.Configuration;

public static class MongoDbConfiguration
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbOptions>(
            configuration.GetSection(nameof(MongoDbOptions)));
        
        services.AddScoped<IMongoDatabase>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoDbOptions>>();
            return new MongoClient(options.Value.ConnectionString).GetDatabase(options.Value.DatabaseName);
        });

        services.AddScoped<IMongoDbService, MongoDbService>();
        
        return services;
    }
}