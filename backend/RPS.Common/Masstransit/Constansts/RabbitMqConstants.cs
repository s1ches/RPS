namespace RPS.Common.Masstransit.Constansts;

public static class RabbitMqConstants
{
    public const string DirectExchangeType = "direct"; 
    
    public const string RegistrationEventsExchangeName = "registration-events-exchange";
    
    public const string RegistrationEventsQueueName = "registration-events";
    
    public const string RegistrationEventsRoutingKey= "registration-events-key";
}