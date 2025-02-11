using Microsoft.EntityFrameworkCore;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Game.Data;
using RPS.Services.Game.Requests.Room.GetRooms;

namespace RPS.Services.Game.Features.Room.Queries.GetRoomsQuery;

public class GetRoomsQueryHandler(GameDbContext dbContext) : IRequestHandler<GetRoomsQuery, GetRoomsResponse>
{
    public Priority Priority { get; set; } = Priority.ExecuteLast;
    
    
    public async Task<GetRoomsResponse> HandleAsync(GetRoomsQuery request, CancellationToken cancellationToken = default)
    {
        var query = dbContext.Rooms
            .OrderBy(x => x.Status)
            .ThenByDescending(x => x.CreateDate)
            .Select(x => new GetRoomsResponseItem
            {
                Id = x.Id,
                MaxAllowedGameRating = x.MaxAllowedGameRating,
                Status = x.Status
            });

        return new GetRoomsResponse
        {
            TotalCount = await query.CountAsync(cancellationToken: cancellationToken),
            Rooms = await query
                .Skip(request.Offset)
                .Take(request.Limit)
                .ToListAsync(cancellationToken: cancellationToken),
        };
    }
}