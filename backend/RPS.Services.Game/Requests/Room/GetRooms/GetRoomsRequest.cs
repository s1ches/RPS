namespace RPS.Services.Game.Requests.Room.GetRooms;

public class GetRoomsRequest
{
    public int Limit { get; set; }
    
    public int Offset { get; set; }
}