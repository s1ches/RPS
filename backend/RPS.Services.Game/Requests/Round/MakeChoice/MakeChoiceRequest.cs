using RPS.Services.Game.Domain.Enums;

namespace RPS.Services.Game.Requests.Round.MakeChoice;

public class MakeChoiceRequest
{
    public long GameId { get; set; }
    
    public long RoundId { get; set; }
    
    public PlayerChoice Choice { get; set; }
}