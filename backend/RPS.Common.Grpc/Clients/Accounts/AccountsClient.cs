using Grpc.Core;

namespace RPS.Common.Grpc.Clients.Accounts;

public class AccountsClient(AccountsService.AccountsServiceClient client) : IAccountsClient
{
    public async Task UpdateUserStatusAsync(long userId, UserStatus userStatus, CancellationToken cancellationToken = default)
    {
        await client.UpdateUserStatusAsync(new UpdateUserStatusRequest
        {
            UserId = userId,
            Status = userStatus
        }, cancellationToken: cancellationToken);
    }

    public async Task<UserStatus> GetUserStatusAsync(long userId, CancellationToken cancellationToken = default)
    {
        var result = await client.GetUserStatusAsync(new GetUserStatusRequest
        {
            UserId = userId
        }, cancellationToken: cancellationToken);
        
        return result.Status;
    }
}