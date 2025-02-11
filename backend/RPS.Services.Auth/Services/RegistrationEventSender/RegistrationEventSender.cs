using MassTransit;
using RPS.Common.Masstransit.Constansts;
using RPS.Common.Masstransit.Events;
using RPS.Services.Auth.Domain.Entities;

namespace RPS.Services.Auth.Services.RegistrationEventSender;

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
        
        await publishEndpoint.Publish(registrationEvent, context =>
        {
            context.SetRoutingKey(RabbitMqConstants.RegistrationEventsRoutingKey);
        }, cancellationToken);    
    }
}