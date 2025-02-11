using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPS.Common.MediatR;
using RPS.Common.Services.ClaimsProvider;
using RPS.Services.Game.Features.Room.Commands.CreateRoomCommand;
using RPS.Services.Game.Features.Room.Commands.JoinRoomCommand;
using RPS.Services.Game.Requests.Room.CreateRoom;

namespace RPS.Services.Game.Controllers;

[Authorize]
[ApiController]
[Route("api/rooms")]
public class RoomController(IMediatr mediatr, IClaimsProvider claimsProvider) : ControllerBase
{
    [HttpPost]
    public async Task<CreateRoomResponse> CreateRoom([FromBody] CreateRoomRequest request, CancellationToken cancellationToken)
    {
        var userId = claimsProvider.GetUserId(HttpContext.User);

        return await mediatr.SendAsync<CreateRoomCommand, CreateRoomResponse>(
            new CreateRoomCommand(userId, request), cancellationToken);
    }

    [HttpPost("join-room")]
    public async Task JoinRoom([FromBody] long roomId, CancellationToken cancellationToken)
    {
        var userId = claimsProvider.GetUserId(HttpContext.User);
        await mediatr.SendAsync(new JoinRoomCommand(userId, roomId), cancellationToken);
    }
}