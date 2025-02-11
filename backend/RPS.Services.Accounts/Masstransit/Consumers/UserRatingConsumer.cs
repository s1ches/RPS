using MassTransit;
using RPS.Common.Exceptions;
using RPS.Common.Masstransit.Events;
using RPS.Services.Accounts.Data.MongoDbService;

namespace RPS.Services.Accounts.Masstransit.Consumers;

public class UserRatingConsumer(ILogger<UserStatusConsumer> logger, IMongoDbService mongoDbService) : IConsumer<UpdateUserRatingEvent>
{
    public async Task Consume(ConsumeContext<UpdateUserRatingEvent> context)
    {
        logger.LogInformation("Received update user rating event, user with id: {id}", context.Message.UserId);
        
        if(!await mongoDbService.IsUserExistsAsync(context.Message.UserId))
            throw new InfrastructureExceptionBase($"User with id doesn't exists: {context.Message.UserId}");
        
        await mongoDbService.UpdateUserRatingAsync(context.Message.UserId, context.Message.Rating);
    }
}