using RPS.Common.Domain.Abstractions;
using RPS.Services.Game.Domain.Enums;

namespace RPS.Services.Game.Domain.Entities;

public class Round : AuditableEntityBase
{
    public long GameId { get; set; }
    
    public GameStatus GameStatus { get; set; }
    
    public long? WinnerId { get; set; }
    
    public PlayerChoice? Player1Choice { get; set; }
    
    public PlayerChoice? Player2Choice { get; set; }
}