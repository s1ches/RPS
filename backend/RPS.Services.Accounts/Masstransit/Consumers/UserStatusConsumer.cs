using MassTransit;
using RPS.Common.Exceptions;
using RPS.Common.Masstransit.Events;
using RPS.Services.Accounts.Data.MongoDbService;
using RPS.Services.Accounts.Domain.Enums;

namespace RPS.Services.Accounts.Masstransit.Consumers;

public class UserStatusConsumer(ILogger<UserStatusConsumer> logger, IMongoDbService mongoDbService)
    : IConsumer<UpdateUserStatusEvent>
{
    public async Task Consume(ConsumeContext<UpdateUserStatusEvent> context)
    {
        logger.LogInformation("Received update user status event, user with id: {id}", context.Message.UserId);
        
        if(!await mongoDbService.IsUserExistsAsync(context.Message.UserId))
            throw new InfrastructureExceptionBase($"User with id doesn't exists: {context.Message.UserId}");
        
        await mongoDbService.UpdateUserStatusAsync(context.Message.UserId, (UserStatus)context.Message.Status);
    }
}