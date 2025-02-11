using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Services.Game.Requests.Room.CreateRoom;

namespace RPS.Services.Game.Features.Room.Commands.CreateRoomCommand;

public class CreateRoomCommand : IRequest<CreateRoomResponse>
{
    public CreateRoomCommand(long creatorId, CreateRoomRequest request)
    {
        CreatorId = creatorId;
        MaxAllowedGameRating = request.MaxAllowedGameRating;
    }
    
    public long CreatorId { get; set; }
    
    public long MaxAllowedGameRating { get; set; }
}