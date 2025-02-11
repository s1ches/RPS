namespace RPS.Common.Masstransit.Events;

public class UpdateUserRatingEvent
{
    public long UserId { get; set; }
    
    public long Rating { get; set; }
}