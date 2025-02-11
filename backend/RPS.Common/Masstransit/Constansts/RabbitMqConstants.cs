namespace RPS.Common.Masstransit.Constansts;

public static class RabbitMqConstants
{
    public const string DirectExchangeType = "direct"; 
    
    public const string RegistrationEventsQueueName = "registration-events";
    
    public const string RegistrationEventsRoutingKey= "registration-events-key";
    
    public const string UpdateUserStatusEventsQueueName = "updat-user-status-events";
    
    public const string UpdateUserStatusEventsRoutingKey= "update-user-status-events-key";
    
    public const string UpdateUserRatingEventsQueueName = "updat-user-rating-events";
    
    public const string UpdateUserRatingEventsRoutingKey= "update-user-rating-events-key";
}