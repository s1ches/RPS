using MassTransit;
using RPS.Common.Grpc;
using RPS.Common.Masstransit.Constansts;
using RPS.Common.Masstransit.Events;

namespace RPS.Services.Game.Services.UpdateUserStatusEventSender;

public class UpdateUserStatusEventSender(ILogger<UpdateUserStatusEventSender> logger, IPublishEndpoint publishEndpoint)
    : IUpdateUserStatusEventSender
{
    public async Task SendEventAsync(long userId, UserStatus userStatus, CancellationToken cancellationToken)
    {
        var updateUserStatusEvent = new UpdateUserStatusEvent
        {
            UserId = userId,
            Status = userStatus
        };

        await publishEndpoint.Publish(updateUserStatusEvent,
            context => { context.SetRoutingKey(RabbitMqConstants.UpdateUserStatusEventsRoutingKey); },
            cancellationToken);

        logger.LogInformation("Update user status event sent, user with id: {id}, status: {status}", userId,
            userStatus);
    }
}