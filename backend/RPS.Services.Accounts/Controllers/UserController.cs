using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPS.Common.Exceptions;
using RPS.Common.MediatR;
using RPS.Services.Accounts.Features.User.Queries.GetUserQuery;
using RPS.Services.Accounts.Features.User.Queries.GetUsersInfosQuery;
using RPS.Services.Accounts.Requests.User.GetUser;
using RPS.Services.Accounts.Requests.User.GetUsersInfos;

namespace RPS.Services.Accounts.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController(IMediatr mediatr) : ControllerBase
{
    [HttpGet]
    public async Task<GetUserResponse> GetUser(CancellationToken cancellationToken)
    {
        var claims = HttpContext.User;
        var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            throw new ApplicationExceptionBase($"Invalid claims of user {claims.FindFirstValue(ClaimTypes.Name)}",
                HttpStatusCode.Unauthorized);

        var parsedUserId = long.Parse(userId);
        return await mediatr.SendAsync<GetUserQuery, GetUserResponse>(new GetUserQuery(parsedUserId),
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