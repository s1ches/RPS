using System.Net;
using Microsoft.EntityFrameworkCore;
using RPS.Common.Exceptions;
using RPS.Common.Grpc;
using RPS.Common.Grpc.Clients.Accounts;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Game.Data;
using RPS.Services.Game.Domain.Enums;

namespace RPS.Services.Game.Features.Room.Commands.JoinRoomCommand;

public class JoinRoomCommandValidator(
    ILogger<JoinRoomCommandValidator> logger,
    GameDbContext dbContext,
    IAccountsClient accountsClient)
    : IValidator<JoinRoomCommand>
{
    public Priority Priority { get; set; } = Priority.ExecuteFirst;

    public async Task HandleAsync(JoinRoomCommand request, CancellationToken cancellationToken = default)
    {
        if (!await dbContext.Rooms.AnyAsync(x => x.Id == request.RoomId && x.Status != RoomStatus.Closed,
                cancellationToken))
        {
            logger.LogInformation(
                "User with id: {userId}, trying to join in closed room, or room doesn't exist with id: {roomId}",
                request.UserId, request.RoomId);
            throw new ApplicationExceptionBase("User cannot join closed room or room doesn't exist", HttpStatusCode.Forbidden);
        }

        if (!await dbContext.Participants.AnyAsync(x => x.UserId == request.UserId, cancellationToken))
            throw new ApplicationExceptionBase($"Participant with userId {request.UserId} doesn't exist",
                HttpStatusCode.Forbidden);
    }
}