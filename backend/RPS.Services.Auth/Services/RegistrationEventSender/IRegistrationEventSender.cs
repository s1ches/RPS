using RPS.Services.Auth.Domain.Entities;

namespace RPS.Services.Auth.Services.RegistrationEventSender;

public interface IRegistrationEventSender
{
    Task SendRegistrationEventAsync(User user, CancellationToken cancellationToken);
}