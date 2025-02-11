using RPS.Services.Game.Domain.Enums;

namespace RPS.Services.Game.Requests.Room.GetRoom;

public class GetRoomResponse
{
    public long RoomId { get; set; }
    
    public long? Player1Id { get; set; }
    
    public long? Player2Id { get; set; }
    
    public long? GameId { get; set; }
    
    public long MaxAllowedGameRating { get; set; }
    
    public RoomStatus RoomStatus { get; set; }
    
    public List<long> SpectatorsIds { get; set; } = [];
}