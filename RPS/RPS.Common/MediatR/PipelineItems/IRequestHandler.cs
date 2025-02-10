using RPS.Common.MediatR.ModelsAbstractions;

namespace RPS.Common.MediatR.PipelineItems;

public interface IRequestHandler<TRequest, TResponse>
    : IPipelineItem<TRequest, TResponse>
    where TRequest : IRequest<TResponse>;

public interface IRequestHandler<TRequest>
    : IPipelineItem<TRequest>
    where TRequest : IRequest;