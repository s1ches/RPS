using RPS.Services.Game.Domain.Enums;

namespace RPS.Services.Game.Requests.Room.GetRooms;

public class GetRoomsResponseItem
{
    public long Id { get; set; }
    
    public long MaxAllowedGameRating { get; set; }
    
    public RoomStatus Status { get; set; }
}