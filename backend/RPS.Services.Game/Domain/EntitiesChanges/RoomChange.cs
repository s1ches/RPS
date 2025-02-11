using RPS.Common.Domain.Abstractions;
using RPS.Services.Game.Domain.Entities;
using RPS.Services.Game.Domain.Enums;

namespace RPS.Services.Game.Domain.EntitiesChanges;

public class RoomChange : EntityChangeBase
{
    public long RoomId { get; set; }

    public Room Room { get; set; } = null!;
    
    public RoomChangeType RoomChangeType {get; set;}
    
    public long? UserId { get; set; }
}