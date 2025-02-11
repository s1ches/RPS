using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Accounts.Data.MongoDbService;
using RPS.Services.Accounts.Requests.User.GetUsersRatings;

namespace RPS.Services.Accounts.Features.User.Queries.GetUsersInfosQuery;

public class GetUsersInfosQueryHandler(IMongoDbService mongoDbService) : IRequestHandler<GetUsersInfosQuery, GetUsersInfosResponse>
{
    public Priority Priority { get; set; } = Priority.ExecuteLast;
    
    public async Task<GetUsersInfosResponse> HandleAsync(GetUsersInfosQuery request, CancellationToken cancellationToken = default)
    {
        var users = await mongoDbService.GetUsersAsync(request.UsersIds, cancellationToken);

        return new GetUsersInfosResponse
        {
            TotalCount = users.Count,
            Users = users.Select(x => new GetUsersInfosResponseItem
            {
                UserId = x.Id,
                UserName = x.UserName,
                Rating = x.Rating,
            }).ToList()
        };
    }
}