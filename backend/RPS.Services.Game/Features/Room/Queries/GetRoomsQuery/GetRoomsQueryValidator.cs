using System.Net;
using Microsoft.EntityFrameworkCore;
using RPS.Common.Exceptions;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Game.Data;
using RPS.Services.Game.Requests.Room.GetRooms;

namespace RPS.Services.Game.Features.Room.Queries.GetRoomsQuery;

public class GetRoomsQueryValidator(GameDbContext dbContext) : IValidator<GetRoomsQuery, GetRoomsResponse>
{
    public Priority Priority { get; set; } = Priority.ExecuteFirst;

    public async Task<GetRoomsResponse> HandleAsync(GetRoomsQuery request,
        CancellationToken cancellationToken = default)
    {
        if (request.Limit <= 0)
            throw new ApplicationExceptionBase("Limit must be greater than 0", HttpStatusCode.BadRequest);

        var gamesCount = await dbContext.Games.CountAsync(cancellationToken: cancellationToken);
        
        if (gamesCount <= request.Offset)
            throw new ApplicationExceptionBase("Offset is more then games we have in database",
                HttpStatusCode.BadRequest);

        return new GetRoomsResponse();
    }
}