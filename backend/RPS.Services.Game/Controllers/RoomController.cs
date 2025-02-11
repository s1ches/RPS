using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPS.Common.Exceptions;
using RPS.Common.MediatR;
using RPS.Services.Game.Features.Room.Commands.CreateRoomCommand;
using RPS.Services.Game.Requests.Room.CreateRoom;

namespace RPS.Services.Game.Controllers;

[Authorize]
[ApiController]
[Route("api/rooms")]
public class RoomController(IMediatr mediatr) : ControllerBase
{
    [HttpPost]
    public async Task<CreateRoomResponse> CreateRoom(CreateRoomRequest request, CancellationToken cancellationToken)
    {
        var claims = HttpContext.User;
        var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            throw new ApplicationExceptionBase($"Invalid claims of user {claims.FindFirstValue(ClaimTypes.Name)}",
                HttpStatusCode.Unauthorized);

        var parsedUserId = long.Parse(userId);

        return await mediatr.SendAsync<CreateRoomCommand, CreateRoomResponse>(
            new CreateRoomCommand(parsedUserId, request), cancellationToken);
    }
}