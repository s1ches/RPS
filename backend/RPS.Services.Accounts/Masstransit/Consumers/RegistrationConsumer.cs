using MassTransit;
using RPS.Common.Exceptions;
using RPS.Common.Masstransit.Events;
using RPS.Services.Accounts.Data.MongoDbService;
using RPS.Services.Accounts.Domain.Entities;

namespace RPS.Services.Accounts.Masstransit.Consumers;

public class RegistrationConsumer(ILogger<RegistrationConsumer> logger, IMongoDbService mongoDbService)
    : IConsumer<RegistrationEvent>
{
    public async Task Consume(ConsumeContext<RegistrationEvent> context)
    {
        logger.LogInformation("Received registration event, user with id: {id} and email {email}", context.Message.Id,
            context.Message);
        
        if(await mongoDbService.IsUserExistsAsync(context.Message.Id))
            throw new InfrastructureExceptionBase($"User with id already exists: {context.Message.Id}");

        var userInfo = new UserInfo
        {
            Id = context.Message.Id,
            UserName = context.Message.UserName,
            CreateDate = context.Message.CreateDate,
            Rating = 0
        };
        
        await mongoDbService.AddUserAsync(userInfo);
    }
}