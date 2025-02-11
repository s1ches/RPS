using Microsoft.EntityFrameworkCore;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Game.Data;
using RPS.Services.Game.Domain.Enums;
using RPS.Services.Game.Services.UpdateUserRatingEventSender;

namespace RPS.Services.Game.Features.Round.Commands.MakeChoiceCommand;

public class MakeChoiceCommandHandler(GameDbContext dbContext, IUpdateUserRatingEventSender updateUserRatingEventSender) : IRequestHandler<MakeChoiceCommand>
{
    public Priority Priority { get; set; } = Priority.ExecuteLast;
    
    public async Task HandleAsync(MakeChoiceCommand request, CancellationToken cancellationToken = default)
    {
        var round = await dbContext.Rounds
            .Include(x => x.Game)
            .SingleAsync(x => x.Id == request.RoundId, cancellationToken);
        
        var choiceDeadline = round.CreateDate.AddSeconds(5);

        if (choiceDeadline < DateTime.Now)
            // TODO:
            throw new NotImplementedException();
            
        if (round.Game.Player1Id == request.UserId)
        {
            round.Player1Choice = request.Choice;
        }
        else
        {
            round.Player2Choice = request.Choice;
        }

        if (round is { Player1Choice: not null, Player2Choice: not null })
        {
            round.Status = GameStatus.Ended;
            if (round.Player1Choice != round.Player2Choice)
            {
                if (round.Player1Choice == PlayerChoice.Scissors)
                {
                    if(round.Player2Choice == PlayerChoice.Rock)
                        round.WinnerId = round.Game.Player2Id;
                    else if(round.Player2Choice == PlayerChoice.Paper)
                        round.WinnerId = round.Game.Player1Id;
                } else if (round.Player1Choice == PlayerChoice.Rock)
                {
                    if(round.Player2Choice == PlayerChoice.Scissors)
                        round.WinnerId = round.Game.Player2Id;
                    else if(round.Player2Choice == PlayerChoice.Paper)
                        round.WinnerId = round.Game.Player1Id;
                }
                    
            }
                
                
        }
    }
}