using System.Net;
using RPS.Common.Exceptions;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Accounts.Data.MongoDbService;
using RPS.Services.Accounts.Requests.User.GetUser;

namespace RPS.Services.Accounts.Features.User.Queries.GetUserQuery;

public class GetUserQueryValidator(ILogger<GetUserQueryValidator> logger, IMongoDbService mongoDbService)
    : IValidator<GetUserQuery, GetUserResponse>
{
    public Priority Priority { get; set; } = Priority.ExecuteFirst;

    public async Task<GetUserResponse> HandleAsync(GetUserQuery request, CancellationToken cancellationToken = default)
    {
        if (request.UserId < 0)
        {
            logger.LogInformation("UserId is invalid {id}", request.UserId);
            throw new ApplicationException("UserId is invalid");
        }

        if (!await mongoDbService.IsUserExistsAsync(request.UserId, cancellationToken: cancellationToken))
        {
            logger.LogInformation("User with id {id} does not exist", request.UserId);
            throw new ApplicationExceptionBase($"User with id {request.UserId} does not exist",
                HttpStatusCode.NotFound);
        }

        logger.LogInformation("GetUserRequest Validated Successfully, user with id {id}", request.UserId);
        return new GetUserResponse();
    }
}