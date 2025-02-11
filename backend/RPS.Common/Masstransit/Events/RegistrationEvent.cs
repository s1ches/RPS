namespace RPS.Common.Masstransit.Events;

public class RegistrationEvent
{
    public long Id { get; set; }
    
    public string UserName { get; set; } = null!;
    
    public DateTime CreateDate { get; set; }
}