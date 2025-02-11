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

namespace RPS.Services.Game.Features.Room.Commands.JoinRoomCommand;

public class JoinRoomCommandHandler(
    ILogger<JoinRoomCommandHandler> logger,
    GameDbContext dbContext,
    IHubContext<RoomHub> roomHub,
    IUpdateUserStatusEventSender updateUserStatusEventSender,
    IAccountsClient accountsClient)
    : IRequestHandler<JoinRoomCommand>
{
    public Priority Priority { get; set; } = Priority.ExecuteLast;

    public async Task HandleAsync(JoinRoomCommand request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("User with id {userId} trying to join room with id: {roomId}", request.UserId,
            request.RoomId);

        var room = await dbContext.Rooms.SingleAsync(x => x.Id == request.RoomId, cancellationToken);
        var participant =
            await dbContext.Participants.SingleAsync(x => x.UserId == request.UserId, cancellationToken);

        participant.Groups.Add(GroupNameHelper.GetGroupName<RoomHub>(room.Id));
        room.Spectators.Add(participant);
        room.RoomChanges.Add(new RoomChange
        {
            RoomChangeType = RoomChangeType.AddSpectator,
            UserId = request.UserId,
            CreateDate = DateTime.UtcNow
        });
        
        dbContext.Rooms.Update(room);
        await dbContext.SaveChangesAsync(cancellationToken);

        await roomHub.Groups.AddToGroupAsync(participant.ConnectionId,
            GroupNameHelper.GetGroupName<RoomHub>(room.Id), cancellationToken);
        
        var user = await accountsClient.GetUserAsync(participant.UserId, cancellationToken);
        if (user.Status != UserStatus.InGame)
            await updateUserStatusEventSender.SendEventAsync(participant.UserId, UserStatus.Spectator,
                cancellationToken);

        var joinRoomModel = new JoinRoomModel
        {
            UserId = participant.UserId,
            UserName = user.UserName,
            Rating = user.Rating,
        };
        
        await roomHub.Clients
            .Group(GroupNameHelper.GetGroupName<RoomHub>(room.Id))
            .SendCoreAsync(RoomHubConstants.JoinRoomMethodName, [joinRoomModel], cancellationToken);
        
        logger.LogInformation("User with id {userId} joined room with id: {roomId}", request.UserId,
            request.RoomId);
    }
}