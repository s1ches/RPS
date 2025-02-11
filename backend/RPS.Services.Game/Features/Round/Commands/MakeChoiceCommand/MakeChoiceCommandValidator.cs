using System.Net;
using Microsoft.EntityFrameworkCore;
using RPS.Common.Exceptions;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Game.Data;

namespace RPS.Services.Game.Features.Round.Commands.MakeChoiceCommand;

public class MakeChoiceCommandValidator(GameDbContext dbContext)
    : IValidator<MakeChoiceCommand>
{
    public Priority Priority { get; set; } = Priority.ExecuteFirst;

    public async Task HandleAsync(MakeChoiceCommand request, CancellationToken cancellationToken = default)
    {
        var round = await dbContext.Rounds
            .Include(x => x.Game)
            .FirstOrDefaultAsync(x => x.Id == request.RoundId, cancellationToken);
        
        if(round == null)
            throw new ApplicationExceptionBase($"Round with id {request.RoundId} not found", HttpStatusCode.NotFound);
        
        if(round.Game.Player1Id != request.UserId && round.Game.Player2Id != request.UserId)
            throw new ApplicationExceptionBase("Only players can make choices", HttpStatusCode.Forbidden);
    }
}