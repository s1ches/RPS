using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Services.Game.Requests.Room.GetRooms;

namespace RPS.Services.Game.Features.Room.Queries.GetRoomsQuery;

public class GetRoomsQuery : IRequest<GetRoomsResponse>
{
    public GetRoomsQuery(GetRoomsRequest request)
    {
        Limit = request.Limit;
        Offset = request.Offset;
    }
    
    public int Limit { get; set; }
    
    public int Offset { get; set; }
}