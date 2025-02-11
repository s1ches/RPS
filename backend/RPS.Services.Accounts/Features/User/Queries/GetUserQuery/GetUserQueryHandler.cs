using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Accounts.Data.MongoDbService;
using RPS.Services.Accounts.Requests.User.GetUser;

namespace RPS.Services.Accounts.Features.User.Queries.GetUserQuery;

public class GetUserQueryHandler(IMongoDbService mongoDbService)
    : IRequestHandler<GetUserQuery, GetUserResponse>
{
    public Priority Priority { get; set; } = Priority.ExecuteLast;

    public async Task<GetUserResponse> HandleAsync(GetUserQuery request, CancellationToken cancellationToken = default)
    {
        var user = await mongoDbService.GetUserAsync(request.UserId, cancellationToken);

        return new GetUserResponse
        {
            Rating = user.Rating,
            UserName = user.UserName,
        };
    }
}