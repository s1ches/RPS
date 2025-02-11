using RPS.Common.Domain.Abstractions;
using RPS.Services.Game.Domain.EntitiesChanges;
using RPS.Services.Game.Domain.Enums;

namespace RPS.Services.Game.Domain.Entities;

public class Room : AuditableEntityBase
{
    public long CreatorId { get; set; }
    
    public RoomStatus Status { get; set; }
    
    public long MaxAllowedGameRating { get; set; }
    
    public List<Game> Games { get; set; } = [];

    public List<Participant> Spectators { get; set; } = [];
    
    public List<RoomChange> RoomChanges { get; set; } = [];
}