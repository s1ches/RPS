using RPS.Services.Auth.Domain.Entities;

namespace RPS.Services.Auth.Services.RegistrationEventSender;

public interface IRegistrationEventSender
{
    Task SendEventAsync(User user, CancellationToken cancellationToken = default);
}