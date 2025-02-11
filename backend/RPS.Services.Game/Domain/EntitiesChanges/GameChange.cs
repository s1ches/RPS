using RPS.Common.Domain.Abstractions;
using RPS.Services.Game.Domain.Enums;

namespace RPS.Services.Game.Domain.EntitiesChanges;

public class GameChange : EntityChangeBase
{
    public long GameId { get; set; }

    public Entities.Game Game { get; set; } = null!;
    
    public GameStatus GameStatus { get; set; }
}