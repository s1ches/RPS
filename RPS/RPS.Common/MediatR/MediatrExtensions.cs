using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RPS.Common.MediatR.Helpers;

namespace RPS.Common.MediatR;

public static class MediatrExtensions
{
    public static IServiceCollection AddMediatR(this IServiceCollection services, Assembly assembly)
    {
        return services
            .RegisterPipelineFromAssembly(assembly)
            .AddScoped<IMediatr, Mediatr>();
    }
}