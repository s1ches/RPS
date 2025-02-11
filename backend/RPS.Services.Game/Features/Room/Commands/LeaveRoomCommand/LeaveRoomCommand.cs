using RPS.Common.MediatR.ModelsAbstractions;

namespace RPS.Services.Game.Features.Room.Commands.LeaveRoomCommand;

public class LeaveRoomCommand : IRequest
{
    public LeaveRoomCommand(long userId, long roomId)
    {
        UserId = userId;
        RoomId = roomId;
    }
    
    public long UserId { get; set; }
    
    public long RoomId { get; set; }
}