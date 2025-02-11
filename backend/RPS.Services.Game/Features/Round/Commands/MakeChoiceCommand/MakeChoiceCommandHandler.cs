using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;

namespace RPS.Services.Game.Features.Round.Commands.MakeChoiceCommand;

public class MakeChoiceCommandHandler : IRequestHandler<MakeChoiceCommand>
{
    public Priority Priority { get; set; } = Priority.ExecuteLast;
    
    public async Task HandleAsync(MakeChoiceCommand request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}