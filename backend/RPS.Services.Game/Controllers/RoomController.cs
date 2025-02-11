using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPS.Common.MediatR;
using RPS.Common.Services.ClaimsProvider;
using RPS.Services.Game.Features.Room.Commands.CreateRoomCommand;
using RPS.Services.Game.Features.Room.Commands.JoinRoomCommand;
using RPS.Services.Game.Features.Room.Commands.LeaveRoomCommand;
using RPS.Services.Game.Features.Room.Queries.GetRoomQuery;
using RPS.Services.Game.Features.Room.Queries.GetRoomsQuery;
using RPS.Services.Game.Requests.Room.CreateRoom;
using RPS.Services.Game.Requests.Room.GetRoom;
using RPS.Services.Game.Requests.Room.GetRooms;

namespace RPS.Services.Game.Controllers;

[Authorize]
[ApiController]
[Route("api/rooms")]
public class RoomController(IMediator mediator, IClaimsProvider claimsProvider) : ControllerBase
{
    [HttpPost]
    public async Task<CreateRoomResponse> CreateRoom([FromBody] CreateRoomRequest request, CancellationToken cancellationToken)
    {
        var userId = claimsProvider.GetUserId(HttpContext.User);

        return await mediator.SendAsync<CreateRoomCommand, CreateRoomResponse>(
            new CreateRoomCommand(userId, request), cancellationToken);
    }

    [HttpPost("join-room")]
    public async Task JoinRoom([FromBody] long roomId, CancellationToken cancellationToken)
    {
        var userId = claimsProvider.GetUserId(HttpContext.User);
        await mediator.SendAsync(new JoinRoomCommand(userId, roomId), cancellationToken);
    }

    [HttpPost("leave-room")]
    public async Task LeaveRoom([FromBody] long roomId, CancellationToken cancellationToken)
    {
        var userId = claimsProvider.GetUserId(HttpContext.User);
        await mediator.SendAsync(new LeaveRoomCommand(userId, roomId), cancellationToken);
    }

    [HttpGet]
    public async Task<GetRoomsResponse> GetRooms(GetRoomsRequest request, CancellationToken cancellationToken)
    {
        return await mediator.SendAsync<GetRoomsQuery, GetRoomsResponse>(new GetRoomsQuery(request), cancellationToken);
    }

    [HttpGet("{id:long}")]
    public async Task<GetRoomResponse> GetRoom(long id, CancellationToken cancellationToken)
    {
        return await mediator.SendAsync<GetRoomQuery, GetRoomResponse>(new GetRoomQuery(id), cancellationToken);
    }
}