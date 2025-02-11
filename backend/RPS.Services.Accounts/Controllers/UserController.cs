using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPS.Common.MediatR;
using RPS.Common.Services.ClaimsProvider;
using RPS.Services.Accounts.Features.User.Queries.GetUserQuery;
using RPS.Services.Accounts.Features.User.Queries.GetUsersInfosQuery;
using RPS.Services.Accounts.Requests.User.GetUser;
using RPS.Services.Accounts.Requests.User.GetUsersInfos;

namespace RPS.Services.Accounts.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController(IMediatr mediatr, IClaimsProvider claimsProvider) : ControllerBase
{
    [HttpGet]
    public async Task<GetUserResponse> GetUser(CancellationToken cancellationToken)
    {
        var userId = claimsProvider.GetUserId(HttpContext.User);
        
        return await mediatr.SendAsync<GetUserQuery, GetUserResponse>(new GetUserQuery(userId),
            cancellationToken);
    }

    [HttpGet("/users-infos")]
    public async Task<GetUsersInfosResponse> GetUsersInfos(GetUsersInfosRequest request,
        CancellationToken cancellationToken)
    {
        return await mediatr.SendAsync<GetUsersInfosQuery, GetUsersInfosResponse>(new GetUsersInfosQuery(request),
            cancellationToken);
    }
}