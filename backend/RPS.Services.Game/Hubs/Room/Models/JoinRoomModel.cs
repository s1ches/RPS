namespace RPS.Services.Game.Hubs.Room.Models;

public class JoinRoomModel
{
    public long UserId { get; set; }
    
    public long Rating { get; set; }

    public string UserName { get; set; } = null!;
}