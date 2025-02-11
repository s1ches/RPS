namespace RPS.Common.MediatR.ModelsAbstractions;

public interface IPipelineItem<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Priority Priority { get; set; }
    
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}

public interface IPipelineItem<in TRequest> where TRequest : IRequest
{
    Priority Priority { get; set; }
    
    Task HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}