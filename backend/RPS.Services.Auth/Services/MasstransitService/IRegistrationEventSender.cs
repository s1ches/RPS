using RPS.Services.Auth.Domain.Entities;

namespace RPS.Services.Auth.Services.MasstransitService;

public interface IRegistrationEventSender
{
    Task SendRegistrationEventAsync(User user, CancellationToken cancellationToken);
}