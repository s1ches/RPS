using Microsoft.Extensions.DependencyInjection;
using RPS.Common.Options;

namespace RPS.Common.Configuration;

public static class CorsConfigurationExtension
{
    public const string CorsPolicyName = "AllowAll";
    
    public static IServiceCollection AddCors(this IServiceCollection services, CorsOptions corsOptions)
    {
        return services.AddCors(opt
            => opt.AddPolicy("AllowAll", policy =>
            {
                policy
                    .WithOrigins(corsOptions.AllowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            })
        );
    }
}