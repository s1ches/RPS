using MassTransit;
using RPS.Common.Masstransit.Events;
using RPS.Services.Auth.Domain.Entities;

namespace RPS.Services.Auth.Services.MasstransitService;

public class RegistrationEventSender(IPublishEndpoint publishEndpoint) : IRegistrationEventSender
{
    public async Task SendRegistrationEventAsync(User user, CancellationToken cancellationToken)
    {
        var registrationEvent = new RegistrationEvent
        {
            Id = user.Id,
            UserName = user.UserName,
            CreateDate = user.CreateDate,
        };
        
        await publishEndpoint.Publish(registrationEvent, cancellationToken);
    }
}