using RPS.Common.MediatR.ModelsAbstractions;

namespace RPS.Services.Game.Features.Game.Commands.JoinGameCommand;

public class JoinGameCommand : IRequest
{
    public JoinGameCommand(long userId, long roomId)
    {
        UserId = userId;
        RoomId = roomId;
    }
    
    public long RoomId { get; set; }
    
    public long UserId { get; set; }
}