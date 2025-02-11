using RPS.Common.Grpc;

namespace RPS.Services.Game.Services.UpdateUserStatusEventSender;

public interface IUpdateUserStatusEventSender
{
    Task SendEventAsync(long userId, UserStatus userStatus, CancellationToken cancellationToken = default);
}