using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Services.Game.Domain.Enums;
using RPS.Services.Game.Requests.Round.MakeChoice;

namespace RPS.Services.Game.Features.Round.Commands.MakeChoiceCommand;

public class MakeChoiceCommand : IRequest
{
    public MakeChoiceCommand(MakeChoiceRequest request)
    {
        GameId = request.GameId;
        RoundId = request.RoundId;
        Choice = request.Choice;
    }
    
    public long GameId { get; set; }
    
    public long RoundId { get; set; }
    
    public PlayerChoice Choice { get; set; }
}