using RPS.Common.MediatR.ModelsAbstractions;

namespace RPS.Common.MediatR.PipelineItems;

public interface IValidator<TRequest>
    : IPipelineItem<TRequest>
    where TRequest : IRequest;

public interface IValidator<TRequest, TResponse>
    : IPipelineItem<TRequest, TResponse>
    where TRequest : IRequest<TResponse>;