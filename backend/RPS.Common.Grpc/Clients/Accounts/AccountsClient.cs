namespace RPS.Common.Grpc.Clients.Accounts;

public class AccountsClient(AccountsService.AccountsServiceClient client) : IAccountsClient
{
    public async Task<GetUserResponse> GetUserAsync(long userId, CancellationToken cancellationToken = default)
    {
        var result = await client.GetUserAsync(new GetUserRequest
        {
            UserId = userId
        }, cancellationToken: cancellationToken);
        
        return result;
    }
}