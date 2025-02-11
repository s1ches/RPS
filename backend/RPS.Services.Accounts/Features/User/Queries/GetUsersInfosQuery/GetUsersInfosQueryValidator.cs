using System.Net;
using RPS.Common.Exceptions;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Accounts.Data.MongoDbService;
using RPS.Services.Accounts.Requests.User.GetUsersRatings;

namespace RPS.Services.Accounts.Features.User.Queries.GetUsersInfosQuery;

public class GetUsersInfosQueryValidator(ILogger<GetUsersInfosQueryValidator> logger, IMongoDbService mongoDbService)
    : IValidator<GetUsersInfosQuery, GetUsersInfosResponse>
{
    public Priority Priority { get; set; } = Priority.ExecuteFirst;

    public async Task<GetUsersInfosResponse> HandleAsync(GetUsersInfosQuery request,
        CancellationToken cancellationToken = default)
    {
        if (!await mongoDbService.IsAllUsersExistsAsync(request.UsersIds, cancellationToken))
        {
            logger.LogInformation("Not all users exist in the database {usersIds}", string.Join(", ", request.UsersIds));
            throw new ApplicationExceptionBase("Not all users exist in the database",
                HttpStatusCode.UnprocessableContent);
        }

        return new GetUsersInfosResponse();
    }
}