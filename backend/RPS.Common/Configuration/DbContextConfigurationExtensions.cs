using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DbContextOptions = RPS.Common.Options.DbContextOptions;

namespace RPS.Common.Configuration;

public static class DbContextConfigurationExtensions
{
    public static IServiceCollection AddDataContext<TDbContext>(this IServiceCollection services,
        DbContextOptions dataContextOptions) where TDbContext : DbContext
    {
        services.AddPooledDbContextFactory<TDbContext>(optionsBuilder =>
            optionsBuilder.UseNpgsql(dataContextOptions.ConnectionString, opts =>
                opts.EnableRetryOnFailure(
                    dataContextOptions.MaxRetryCountOnFailure,
                    TimeSpan.FromSeconds(dataContextOptions.RetryDelayInSeconds),
                    null
                )
            )
        );

        services.AddDbContextPool<DbContext, TDbContext>(optionsBuilder =>
            optionsBuilder.UseNpgsql(dataContextOptions.ConnectionString,
                opts =>
                    opts.EnableRetryOnFailure(
                        dataContextOptions.MaxRetryCountOnFailure,
                        TimeSpan.FromSeconds(dataContextOptions.RetryDelayInSeconds),
                        null
                    )
            )
        );
        
        return services;
    }
}