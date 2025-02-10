using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using RPS.Common.MediatR.ModelsAbstractions;

namespace RPS.Common.MediatR.Helpers;

internal static class MediatrRegisterHelper
{
    internal static IServiceCollection RegisterPipelineFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var handlers = assembly.GetTypes()
            .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IPipelineItem<>) ||
                                              i.GetGenericTypeDefinition() == typeof(IPipelineItem<,>))));

        foreach (var handler in handlers)
        {
            var implementedInterface = handler.GetInterfaces()
                .First(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IPipelineItem<>) ||
                                                i.GetGenericTypeDefinition() == typeof(IPipelineItem<,>)));
            services.AddTransient(implementedInterface, handler);
        }

        return services;
    }
}