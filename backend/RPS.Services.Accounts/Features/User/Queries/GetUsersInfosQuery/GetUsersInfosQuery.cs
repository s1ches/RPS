using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Services.Accounts.Requests.User.GetUsersRatings;

namespace RPS.Services.Accounts.Features.User.Queries.GetUsersInfosQuery;

public class GetUsersInfosQuery : IRequest<GetUsersInfosResponse>
{
    public GetUsersInfosQuery(GetUsersInfosRequest request)
    {
        UsersIds = request.UserIds;
    }
    
    public List<long> UsersIds { get; set; }
}