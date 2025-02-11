using RPS.Common.Grpc;

namespace RPS.Common.Masstransit.Events;

public class UpdateUserStatusEvent
{
    public long UserId { get; set; }
    
    public UserStatus Status { get; set; }
}