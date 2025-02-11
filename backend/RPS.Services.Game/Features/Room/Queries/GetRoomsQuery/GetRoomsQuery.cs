using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Services.Game.Requests.Room.GetRooms;

namespace RPS.Services.Game.Features.Room.Queries.GetRoomsQuery;

public class GetRoomsQuery : IRequest<GetRoomsResponse>
{
    public GetRoomsQuery(int limit, int offset)
    {
        Limit = limit;
        Offset = offset;
    }
    
    public int Limit { get; set; }
    
    public int Offset { get; set; }
}