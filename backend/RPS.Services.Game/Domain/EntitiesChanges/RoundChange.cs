using RPS.Common.Domain.Abstractions;
using RPS.Services.Game.Domain.Entities;
using RPS.Services.Game.Domain.Enums;

namespace RPS.Services.Game.Domain.EntitiesChanges;

public class RoundChange : EntityChangeBase
{
    public long RoundId { get; set; }

    public Round Round { get; set; } = null!;
    
    public GameStatus RoundStatus { get; set; }
}