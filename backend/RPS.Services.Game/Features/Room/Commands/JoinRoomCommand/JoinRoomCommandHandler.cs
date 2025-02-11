using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RPS.Common.Grpc;
using RPS.Common.Grpc.Clients.Accounts;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Game.Data;
using RPS.Services.Game.Hubs;
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

        room.Spectators.Add(participant);

        await dbContext.SaveChangesAsync(cancellationToken);

        await roomHub.Groups.AddToGroupAsync(participant.ConnectionId,
            GroupNameHelper.GetGroupName<RoomHub>(room.Id), cancellationToken);

        var userStatus = await accountsClient.GetUserStatusAsync(participant.UserId, cancellationToken);
        if (userStatus != UserStatus.InGame)
            await updateUserStatusEventSender.SendEventAsync(participant.UserId, UserStatus.Spectator,
                cancellationToken);
    }
}