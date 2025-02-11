using RPS.Common.Domain.Abstractions;

namespace RPS.Services.Game.Domain.Entities;

public class Participant : EntityBase
{
    public long UserId { get; set; }
    
    public string ConnectionId { get; set; } = null!;
    
    public List<string> Groups { get; set; } = [];
    
    public List<Room> Rooms { get; set; } = null!;
}