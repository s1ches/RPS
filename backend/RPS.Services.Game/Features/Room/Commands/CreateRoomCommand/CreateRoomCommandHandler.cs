using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Game.Data;
using RPS.Services.Game.Domain.EntitiesChanges;
using RPS.Services.Game.Domain.Enums;
using RPS.Services.Game.Requests.Room.CreateRoom;

namespace RPS.Services.Game.Features.Room.Commands.CreateRoomCommand;

public class CreateRoomCommandHandler(GameDbContext dbContext)
    : IRequestHandler<CreateRoomCommand, CreateRoomResponse>
{
    public Priority Priority { get; set; } = Priority.ExecuteLast;

    public async Task<CreateRoomResponse> HandleAsync(CreateRoomCommand request,
        CancellationToken cancellationToken = default)
    {
        var room = new Domain.Entities.Room
        {
            Status = RoomStatus.WaitingForPlayer,
            CreatorId = request.CreatorId,
            MaxAllowedGameRating = request.MaxAllowedGameRating,
            UpdateDate = DateTime.UtcNow,
            CreateDate = DateTime.UtcNow,
            RoomChanges =
            [
                new RoomChange
                {
                    CreateDate = DateTime.UtcNow,
                    RoomChangeType = RoomChangeType.Created,
                    UserId = request.CreatorId
                }
            ]
        };

        var entry = await dbContext.Rooms.AddAsync(room, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateRoomResponse
        {
            RoomId = entry.Entity.Id
        };
    }
}