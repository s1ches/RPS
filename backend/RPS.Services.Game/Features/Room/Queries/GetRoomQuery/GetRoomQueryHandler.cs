using Microsoft.EntityFrameworkCore;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Game.Data;
using RPS.Services.Game.Domain.Enums;
using RPS.Services.Game.Requests.Room.GetRoom;

namespace RPS.Services.Game.Features.Room.Queries.GetRoomQuery;

public class GetRoomQueryHandler(GameDbContext dbContext) : IRequestHandler<GetRoomQuery, GetRoomResponse>
{
    public Priority Priority { get; set; } = Priority.ExecuteLast;
    
    public async Task<GetRoomResponse> HandleAsync(GetRoomQuery request, CancellationToken cancellationToken = default)
    {
        var room = await dbContext.Rooms
            .Include(x => x.Games.FirstOrDefault(g =>
                g.Status == GameStatus.Started))
            .ThenInclude(x => x.Rounds)
            .Include(room => room.Spectators)
            .SingleAsync(x => x.Id == request.Id, cancellationToken);

        return new GetRoomResponse
        {
            GameId = room.Games.First().Id,
            RoomId = room.Id,
            Player1Id = room.Games.FirstOrDefault()?.Player1Id,
            Player2Id = room.Games.FirstOrDefault()?.Player2Id,
            SpectatorsIds = room.Spectators.Select(x => x.Id).ToList(),
            RoomStatus = room.Status,
            MaxAllowedGameRating = room.MaxAllowedGameRating
        };
    }
}