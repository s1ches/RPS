using Microsoft.Extensions.DependencyInjection;
using RPS.Common.Exceptions;
using RPS.Common.MediatR.ModelsAbstractions;

namespace RPS.Common.MediatR;

public class Mediator(IServiceScopeFactory serviceScopeFactory) : IMediator
{
    public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request,
        CancellationToken cancellationToken = default)
        where TRequest : IRequest<TResponse>
    {
        using var scope = serviceScopeFactory.CreateScope();

        var pipelineItems = scope.ServiceProvider.GetServices(typeof(IPipelineItem<TRequest, TResponse>))
            .Select(x => x as IPipelineItem<TRequest, TResponse>
                         ?? throw new InvalidOperationException("No pipeline items registered"))
            .OrderBy(x => x.Priority)
            .ToList();
        
        if(!IsValidPriority(pipelineItems))
            throw new InfrastructureExceptionBase(
                $"Duplicate priority error occurred, while handling {typeof(TRequest).Name}");

        for (var i = 0; i < pipelineItems.Count - 1; i++)
            await pipelineItems[i].HandleAsync(request, cancellationToken);

        return await pipelineItems[^1].HandleAsync(request, cancellationToken);
    }

    public async Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest
    {
        using var scope = serviceScopeFactory.CreateScope();

        var pipelineItems = scope.ServiceProvider.GetServices(typeof(IPipelineItem<TRequest>))
            .Select(x => x as IPipelineItem<TRequest>
                         ?? throw new InvalidOperationException("No pipeline items registered"))
            .OrderBy(x => x.Priority)
            .ToList();
        
        if(!IsValidPriority(pipelineItems))
            throw new InfrastructureExceptionBase(
                $"Duplicate priority error occurred, while handling {typeof(TRequest).Name}");

        foreach (var pipelineItem in pipelineItems)
            await pipelineItem.HandleAsync(request, cancellationToken);
    }

    private static bool IsValidPriority<TRequest, TResponse>(List<IPipelineItem<TRequest, TResponse>> pipelineItems)
        where TRequest : IRequest<TResponse>
    {
        var hashSet = new HashSet<Priority>(pipelineItems.Count);

        return pipelineItems.All(pipelineItem => hashSet.Add(pipelineItem.Priority));
    }
    
    private static bool IsValidPriority<TRequest>(List<IPipelineItem<TRequest>> pipelineItems)
        where TRequest : IRequest
    {
        var hashSet = new HashSet<Priority>(pipelineItems.Count);

        return pipelineItems.All(pipelineItem => hashSet.Add(pipelineItem.Priority));
    }
}