using RPS.Services.Game.Domain.Enums;

namespace RPS.Services.Game.Hubs.Room.Models;

public class JoinGameModel
{
    public long PlayerId { get; set; }
    
    public long GameId { get; set; }
    
    public GameStatus GameStatus { get; set; }
}