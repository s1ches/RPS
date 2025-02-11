using System.Net;
using Microsoft.EntityFrameworkCore;
using RPS.Common.Exceptions;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Game.Data;

namespace RPS.Services.Game.Features.Room.Commands.LeaveRoomCommand;

public class LeaveRoomCommandValidator(ILogger<LeaveRoomCommandValidator> logger, GameDbContext dbContext)
    : IValidator<LeaveRoomCommand>
{
    public Priority Priority { get; set; } = Priority.ExecuteFirst;

    public async Task HandleAsync(LeaveRoomCommand request, CancellationToken cancellationToken = default)
    {
        if (!await dbContext.Rooms.AnyAsync(x => x.Id == request.RoomId,
                cancellationToken))
        {
            logger.LogInformation(
                "User with id: {userId}, trying to leave room with id: {roomId}, this one doesn't exist",
                request.UserId, request.RoomId);
            throw new ApplicationExceptionBase("User cannot join room, which doesn't exits", HttpStatusCode.Forbidden);
        }
    }
}