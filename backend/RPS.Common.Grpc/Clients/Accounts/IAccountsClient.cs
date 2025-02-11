namespace RPS.Common.Grpc.Clients.Accounts;

public interface IAccountsClient
{
    Task UpdateUserStatusAsync(long userId, UserStatus userStatus, CancellationToken cancellationToken = default);
    
    Task<UserStatus> GetUserStatusAsync(long userId, CancellationToken cancellationToken = default);
}