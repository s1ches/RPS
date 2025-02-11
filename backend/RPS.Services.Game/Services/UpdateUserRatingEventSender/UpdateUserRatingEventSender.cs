using MassTransit;
using RPS.Common.Masstransit.Constansts;
using RPS.Common.Masstransit.Events;

namespace RPS.Services.Game.Services.UpdateUserRatingEventSender;

public class UpdateUserRatingEventSender(ILogger<UpdateUserRatingEventSender> logger, IPublishEndpoint publishEndpoint)
    : IUpdateUserRatingEventSender
{
    public async Task SendEventAsync(long userId, long addRating, CancellationToken cancellationToken)
    {
        var updateUserStatusEvent = new UpdateUserRatingEvent
        {
            UserId = userId,
            AddRating = addRating
        };

        await publishEndpoint.Publish(updateUserStatusEvent,
            context => { context.SetRoutingKey(RabbitMqConstants.UpdateUserRatingEventsRoutingKey); },
            cancellationToken);

        logger.LogInformation("Update user status event sent, user with id: {id}, add rating: {addRating}", userId,
            addRating);
    }
}