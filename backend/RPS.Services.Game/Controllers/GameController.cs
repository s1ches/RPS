using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPS.Common.MediatR;
using RPS.Common.Services.ClaimsProvider;
using RPS.Services.Game.Features.Game.Commands.JoinGameCommand;

namespace RPS.Services.Game.Controllers;

[Authorize]
[ApiController]
[Route("api/games")]
public class GameController(IMediator mediator, IClaimsProvider claimsProvider) : ControllerBase
{
    [HttpPost("join-game")]
    public async Task JoinGame([FromBody] long roomId, CancellationToken cancellationToken)
    {
        var userId = claimsProvider.GetUserId(HttpContext.User);
        await mediator.SendAsync(new JoinGameCommand(userId, roomId), cancellationToken);
    }
}