using MassTransit;
using RPS.Common.Masstransit.Constansts;
using RPS.Common.Masstransit.Events;
using RPS.Services.Auth.Domain.Entities;

namespace RPS.Services.Auth.Services.RegistrationEventSender;

public class RegistrationEventSender(ILogger<RegistrationEventSender> logger, IPublishEndpoint publishEndpoint)
    : IRegistrationEventSender
{
    public async Task SendEventAsync(User user, CancellationToken cancellationToken)
    {
        var registrationEvent = new RegistrationEvent
        {
            Id = user.Id,
            UserName = user.UserName,
            CreateDate = user.CreateDate
        };

        await publishEndpoint.Publish(registrationEvent,
            context => { context.SetRoutingKey(RabbitMqConstants.RegistrationEventsRoutingKey); }, cancellationToken);
        
        logger.LogInformation("Registration event sent, user with id: {id}", user.Id);
    }
}