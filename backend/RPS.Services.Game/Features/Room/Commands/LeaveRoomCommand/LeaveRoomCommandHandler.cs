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

namespace RPS.Services.Game.Features.Room.Commands.LeaveRoomCommand;

public class LeaveRoomCommandHandler(
    ILogger<LeaveRoomCommandHandler> logger,
    GameDbContext dbContext,
    IHubContext<RoomHub> roomHub,
    IUpdateUserStatusEventSender updateUserStatusEventSender,
    IAccountsClient accountsClient) : IRequestHandler<LeaveRoomCommand>
{
    public Priority Priority { get; set; } = Priority.ExecuteLast;

    public async Task HandleAsync(LeaveRoomCommand request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("User with id {userId} trying to leave room with id: {roomId}", request.UserId,
            request.RoomId);

        var room = await dbContext.Rooms
            .Include(x => x.Games.FirstOrDefault(g =>
                g.Status == GameStatus.Started && (g.Player1Id == request.UserId || g.Player2Id == request.UserId)))
            .ThenInclude(x => x.Rounds)
            .SingleAsync(x => x.Id == request.RoomId, cancellationToken);
        var isPlayer = room.Games.First().Player1Id == request.UserId || room.Games.First().Player2Id == request.UserId;
        var participant =
            await dbContext.Participants.SingleAsync(x => x.UserId == request.UserId, cancellationToken);


        participant.Groups.Add(GroupNameHelper.GetGroupName<RoomHub>(room.Id));
        room.Spectators.Add(participant);
        room.RoomChanges.Add(new RoomChange
        {
            RoomChangeType = isPlayer ? RoomChangeType.LeavePlayer : RoomChangeType.LeaveSpectator,
            UserId = request.UserId,
            CreateDate = DateTime.UtcNow
        });

        room.Status = RoomStatus.WaitingForPlayer;
        
        await roomHub.Groups.AddToGroupAsync(participant.ConnectionId,
            GroupNameHelper.GetGroupName<RoomHub>(room.Id), cancellationToken);

        if (isPlayer)
        {
            var user = await accountsClient.GetUserAsync(participant.UserId, cancellationToken);
            if (user.Status != UserStatus.InGame)
                await updateUserStatusEventSender.SendEventAsync(participant.UserId, UserStatus.Online,
                    cancellationToken);

            var game = room.Games.First();
            var round = game.Rounds.MaxBy(x => x.CreateDate);

            var player1 = game.Player1Id;
            var player2 = game.Player2Id;

            var player1WinRoundsCount = game.Rounds.Count(x => x.WinnerId != 0 && x.WinnerId == player1);
            var player2WinRoundsCount = game.Rounds.Count(x => x.WinnerId != 0 && x.WinnerId == player2);

            var hasWinner = player1WinRoundsCount == player2WinRoundsCount;
            int? winnerId = player1WinRoundsCount > player2WinRoundsCount
                ? player1WinRoundsCount
                : player2WinRoundsCount;

            if (!hasWinner)
                winnerId = null;
            
            game.WinnerId = winnerId;
            
            game.GameChanges.Add(new GameChange
            {
                GameStatus = GameStatus.Ended,
                CreateDate = DateTime.UtcNow
            });

            round!.RoundChanges.Add(new RoundChange
            {
                RoundStatus = GameStatus.Ended,
                CreateDate = DateTime.UtcNow
            });
            
            dbContext.Games.Update(game);
            dbContext.Rooms.Update(room);

            var knowWinnerModel = new KnowWinnerModel
            {
                WinnerId = winnerId
            };
            
            await roomHub.Clients
                .Group(GroupNameHelper.GetGroupName<RoomHub>(room.Id))
                .SendCoreAsync(RoomHubConstants.LeaveRoomMethodName, [knowWinnerModel], cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        var leaveRoomModel = new LeaveRoomModel
        {
            IsPlayer = isPlayer,
            UserId = participant.UserId
        };

        await roomHub.Clients
            .Group(GroupNameHelper.GetGroupName<RoomHub>(room.Id))
            .SendCoreAsync(RoomHubConstants.LeaveRoomMethodName, [leaveRoomModel], cancellationToken);

        logger.LogInformation("User with id {userId} joined room with id: {roomId}", request.UserId,
            request.RoomId);
    }
}