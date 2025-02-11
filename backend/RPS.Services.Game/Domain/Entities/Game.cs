using RPS.Common.Domain.Abstractions;
using RPS.Services.Game.Domain.EntitiesChanges;
using RPS.Services.Game.Domain.Enums;

namespace RPS.Services.Game.Domain.Entities;

public class Game : AuditableEntityBase
{
    public long Player1Id { get; set; }
    
    public long? Player2Id { get; set; }
    
    public GameStatus Status { get; set; }
    
    public long? WinnerId { get; set; }
    
    public long RoomId { get; set; }

    public Room Room { get; set; } = null!;

    public List<Round> Rounds { get; set; } = [];

    public List<GameChange> GameChanges { get; set; } = [];
}