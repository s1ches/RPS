using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using RPS.Common.Grpc;
using RPS.Services.Accounts.Data.MongoDbService;

namespace RPS.Services.Accounts.GrpcServer;

public class AccountsServer(ILogger<AccountsServer> logger, IMongoDbService mongoDbService)
    : AccountsService.AccountsServiceBase
{
    public override async Task<GetUserStatusResponse> GetUserStatus(GetUserStatusRequest request,
        ServerCallContext context)
    {
        logger.LogInformation("Get user status request received, for user with id {id}", request.UserId);

        if (!await mongoDbService.IsUserExistsAsync(request.UserId, context.CancellationToken))
            throw new RpcException(new Status(StatusCode.NotFound,
                $"User with id: {request.UserId} does not exist"));

        var user = await mongoDbService.GetUserAsync(request.UserId, context.CancellationToken);

        return new GetUserStatusResponse
        {
            Status = (UserStatus)user.Status,
        };
    }

    // TODO: Maybe remove this
    public override Task<Empty> UpdateUserStatus(UpdateUserStatusRequest request, ServerCallContext context)
    {
        return base.UpdateUserStatus(request, context);
    }
}