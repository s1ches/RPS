using System.Net;
using Microsoft.EntityFrameworkCore;
using RPS.Common.Exceptions;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Game.Data;
using RPS.Services.Game.Requests.Room.GetRoom;

namespace RPS.Services.Game.Features.Room.Queries.GetRoomQuery;

public class GetRoomQueryValidator(GameDbContext dbContext) : IValidator<GetRoomQuery, GetRoomResponse>
{
    public Priority Priority { get; set; } = Priority.ExecuteFirst;
    
    public async Task<GetRoomResponse> HandleAsync(GetRoomQuery request, CancellationToken cancellationToken = default)
    {
        if(!await dbContext.Rooms.AnyAsync(x => x.Id == request.Id, cancellationToken: cancellationToken))
            throw new ApplicationExceptionBase("Room does not exist", HttpStatusCode.NotFound);

        return new GetRoomResponse();
    }
}