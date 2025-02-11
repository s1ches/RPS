namespace RPS.Common.Grpc.Clients.Accounts;

public interface IAccountsClient
{
    Task<GetUserResponse> GetUserAsync(long userId, CancellationToken cancellationToken = default);
}