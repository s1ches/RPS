namespace RPS.Services.Game.Hubs.Room.Models;

public class LeaveRoomModel
{
    public bool IsPlayer { get; set; }
    
    public long UserId { get; set; }
}