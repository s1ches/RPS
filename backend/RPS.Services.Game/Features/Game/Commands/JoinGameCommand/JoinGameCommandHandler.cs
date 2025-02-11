using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RPS.Common.Grpc;
using RPS.Common.Grpc.Clients.Accounts;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Game.Data;
using RPS.Services.Game.Domain.EntitiesChanges;
using RPS.Services.Game.Domain.Enums;
using RPS.Services.Game.Hubs.Room;
using RPS.Services.Game.Hubs.Room.Models;
using RPS.Services.Game.Services.UpdateUserStatusEventSender;

namespace RPS.Services.Game.Features.Game.Commands.JoinGameCommand;

public class JoinGameCommandHandler(
    ILogger<JoinGameCommandHandler> logger,
    GameDbContext dbContext,
    IHubContext<RoomHub> roomHub,
    IUpdateUserStatusEventSender updateUserStatusEventSender) : IRequestHandler<JoinGameCommand>
{
    public Priority Priority { get; set; } = Priority.ExecuteLast;
    
    public async Task HandleAsync(JoinGameCommand request, CancellationToken cancellationToken = default)
    {
        var room = await dbContext.Rooms
            .Include(x => x.Games)
            .SingleAsync(x => x.Id == request.RoomId, cancellationToken);

        var game = room.Games
            .Where(x => x.Status == GameStatus.WaitingForPlayer)
            .OrderByDescending(x => x.CreateDate)
            .FirstOrDefault();

        if (game == null)
        {
            game = new Domain.Entities.Game
            {
                CreateDate = DateTime.UtcNow,
                Status = GameStatus.WaitingForPlayer,
                Player1Id = request.UserId,
                GameChanges =
                [
                    new GameChange
                    {
                        GameStatus = GameStatus.WaitingForPlayer,
                        CreateDate = DateTime.UtcNow,
                    }
                ]
            };
            
            room.Games.Add(game);
            dbContext.Update(room);
        }
        else
        {
            game.Player2Id = request.UserId;
            game.Status = GameStatus.Started;
            game.GameChanges.Add(new GameChange
            {
                GameStatus = GameStatus.Started,
                CreateDate = DateTime.UtcNow,
            });
            dbContext.Update(game);
        }
        
        await dbContext.SaveChangesAsync(cancellationToken);

        var joinGameModel = new JoinGameModel
        {
            GameId = game.Id,
            GameStatus = game.Status,
            PlayerId = request.UserId
        };

        await updateUserStatusEventSender.SendEventAsync(request.UserId, UserStatus.InGame, cancellationToken);
        
        await roomHub.Clients.Group(GroupNameHelper.GetGroupName<RoomHub>(room.Id))
            .SendCoreAsync(RoomHubConstants.JoinGame, [joinGameModel], cancellationToken);
    }
}