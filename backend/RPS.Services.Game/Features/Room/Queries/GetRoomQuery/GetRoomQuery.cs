using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Services.Game.Requests.Room.GetRoom;

namespace RPS.Services.Game.Features.Room.Queries.GetRoomQuery;

public class GetRoomQuery : IRequest<GetRoomResponse>
{
    public GetRoomQuery(long roomId)
    {
        Id = roomId;
    }
    
    public long Id { get; set; }
}