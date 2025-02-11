using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;

namespace RPS.Services.Game.Features.Round.Commands.MakeChoiceCommand;

public class MakeChoiceCommandValidator : IValidator<MakeChoiceCommand>
{
    public Priority Priority { get; set; } = Priority.ExecuteFirst;
    
    public async Task HandleAsync(MakeChoiceCommand request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}