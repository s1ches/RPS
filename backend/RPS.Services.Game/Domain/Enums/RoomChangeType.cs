namespace RPS.Services.Game.Domain.Enums;

public enum RoomChangeType
{
    AddSpectator,
    LeaveSpectator,
    AddPlayer,
    LeavePlayer,
    Created,
    Closed
}