using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Services.Accounts.Requests.User.GetUser;

namespace RPS.Services.Accounts.Features.User.Queries.GetUserQuery;

public class GetUserQuery : IRequest<GetUserResponse>
{
    public GetUserQuery(long userId)
    {
        UserId = userId;
    }
    
    public long UserId { get; set; }
}