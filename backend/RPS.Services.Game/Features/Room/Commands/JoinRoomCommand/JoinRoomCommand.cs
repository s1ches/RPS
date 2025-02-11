using RPS.Common.MediatR.ModelsAbstractions;

namespace RPS.Services.Game.Features.Room.Commands.JoinRoomCommand;

public class JoinRoomCommand : IRequest
{
    public JoinRoomCommand(long userId, long roomId)
    {
        UserId = userId;
        RoomId = roomId;
    }
    
    public long UserId { get; set; }
    
    public long RoomId { get; set; }
}