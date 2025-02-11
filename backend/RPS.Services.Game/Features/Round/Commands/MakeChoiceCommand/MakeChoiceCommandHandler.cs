using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Game.Data;
using RPS.Services.Game.Domain.EntitiesChanges;
using RPS.Services.Game.Domain.Enums;
using RPS.Services.Game.Hubs.Room;
using RPS.Services.Game.Hubs.Room.Models;
using RPS.Services.Game.Services.UpdateUserRatingEventSender;

namespace RPS.Services.Game.Features.Round.Commands.MakeChoiceCommand;

public class MakeChoiceCommandHandler(
    GameDbContext dbContext,
    IUpdateUserRatingEventSender updateUserRatingEventSender,
    IHubContext<RoomHub> hubContext)
    : IRequestHandler<MakeChoiceCommand>
{
    public Priority Priority { get; set; } = Priority.ExecuteLast;

    public async Task HandleAsync(MakeChoiceCommand request, CancellationToken cancellationToken = default)
    {
        var round = await dbContext.Rounds
            .Include(x => x.Game)
            .SingleAsync(x => x.Id == request.RoundId, cancellationToken);

        var choiceDeadline = round.CreateDate.AddSeconds(5);

        if (choiceDeadline < DateTime.Now)
        {
            if (round.Player2Choice != null || round.Player1Choice != null)
                round.WinnerId = round.Player1Choice != null ? round.Game.Player1Id : round.Game.Player2Id;
        }

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
                    if (round.Player2Choice == PlayerChoice.Rock)
                        round.WinnerId = round.Game.Player2Id;
                    else if (round.Player2Choice == PlayerChoice.Paper)
                        round.WinnerId = round.Game.Player1Id;
                }
                else if (round.Player1Choice == PlayerChoice.Rock)
                {
                    if (round.Player2Choice == PlayerChoice.Scissors)
                        round.WinnerId = round.Game.Player1Id;
                    else if (round.Player2Choice == PlayerChoice.Paper)
                        round.WinnerId = round.Game.Player2Id;
                }
                else if (round.Player1Choice == PlayerChoice.Paper)
                {
                    if (round.Player2Choice == PlayerChoice.Scissors)
                        round.WinnerId = round.Game.Player2Id;
                    else if (round.Player2Choice == PlayerChoice.Rock)
                        round.WinnerId = round.Game.Player1Id;
                }
            }
        }

        round.Status = GameStatus.Ended;
        round.RoundChanges.Add(new RoundChange
        {
            CreateDate = DateTime.UtcNow,
            RoundStatus = GameStatus.Ended
        });

        var newRound = new Domain.Entities.Round
        {
            GameId = round.GameId,
            Status = GameStatus.Started,
            CreateDate = DateTime.UtcNow + TimeSpan.FromSeconds(5)
        };

        dbContext.Rounds.Add(newRound);
        dbContext.Update(round);
        await dbContext.SaveChangesAsync(cancellationToken);

        if (round.WinnerId != null)
        {
            await updateUserRatingEventSender.SendEventAsync(round.WinnerId.Value, 3, cancellationToken);

            var looserId = round.Game.Player1Id == round.WinnerId ? round.Game.Player2Id : round.Game.Player1Id;
            await updateUserRatingEventSender.SendEventAsync(looserId!.Value, -1, cancellationToken);
        }

        var knowRoundWinner = new KnowRoundWinnerModel
        {
            RoundWinnerId = round.WinnerId
        };
        
        await hubContext.Clients
            .Group(GroupNameHelper.GetGroupName<RoomHub>(round.Game.RoomId))
            .SendCoreAsync(RoomHubConstants.KnowGameWinner,[knowRoundWinner], cancellationToken);
    }
}