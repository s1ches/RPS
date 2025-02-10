using Microsoft.Extensions.DependencyInjection;
using RPS.Common.MediatR.ModelsAbstractions;

namespace RPS.Common.MediatR;

public class Mediatr(IServiceScopeFactory serviceScopeFactory) : IMediatr
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
        
        for (var i = 0; i < pipelineItems.Count - 1; i++)
            await pipelineItems[i].HandleAsync(request, cancellationToken);
        
        return await pipelineItems[^1].HandleAsync(request, cancellationToken);
    }

    public async Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        using var scope = serviceScopeFactory.CreateScope();

        var pipelineItems = scope.ServiceProvider.GetServices(typeof(IPipelineItem<TRequest>))
            .Select(x => x as IPipelineItem<TRequest> 
                         ?? throw new InvalidOperationException("No pipeline items registered"))
            .OrderBy(x => x.Priority)
            .ToList();
        
        foreach (var pipelineItem in pipelineItems)
            await pipelineItem.HandleAsync(request, cancellationToken);
    }
}