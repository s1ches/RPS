namespace RPS.Services.Game.Requests.Room.GetRooms;

public class GetRoomsResponse
{
    public List<GetRoomsResponseItem> Rooms { get; set; } = [];
    
    public long TotalCount { get; set; }
}