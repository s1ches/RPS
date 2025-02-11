using System.Net;
using Microsoft.EntityFrameworkCore;
using RPS.Common.Exceptions;
using RPS.Common.Grpc;
using RPS.Common.Grpc.Clients.Accounts;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Game.Data;
using RPS.Services.Game.Domain.Enums;

namespace RPS.Services.Game.Features.Game.Commands.JoinGameCommand;

public class JoinGameCommandValidator(
    ILogger<JoinGameCommandValidator> logger,
    GameDbContext dbContext,
    IAccountsClient accountsClient)
    : IValidator<JoinGameCommand>
{
    public Priority Priority { get; set; } = Priority.ExecuteFirst;

    public async Task HandleAsync(JoinGameCommand request, CancellationToken cancellationToken = default)
    {
        if (!await dbContext.Rooms.AnyAsync(x => x.Id == request.RoomId
                                                 && x.Status != RoomStatus.Closed
                                                 && x.Status == RoomStatus.GameInProgress,
                cancellationToken))
        {
            logger.LogInformation(
                "User with id: {userId}, trying to join game in closed room, or room doesn't exist with id: {roomId}, or game already started",
                request.UserId, request.RoomId);
            throw new ApplicationExceptionBase(
                "User cannot join game in closed room or room doesn't exist, or game already started",
                HttpStatusCode.Forbidden);
        }

        var user = await accountsClient.GetUserAsync(request.UserId, cancellationToken);
        if (user.Status == UserStatus.InGame)
            throw new ApplicationExceptionBase("User already in another game", HttpStatusCode.Forbidden);

        var room = await dbContext.Rooms.SingleAsync(x => x.Id == request.RoomId, cancellationToken);
        if (user.Rating > room.MaxAllowedGameRating)
            throw new ApplicationExceptionBase(
                $"User has rating more than max allowed, max allowed is: {room.MaxAllowedGameRating}",
                HttpStatusCode.Forbidden);
    }
}