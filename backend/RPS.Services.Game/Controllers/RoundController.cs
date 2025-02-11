using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPS.Common.MediatR;
using RPS.Common.Services.ClaimsProvider;
using RPS.Services.Game.Features.Round.Commands.MakeChoiceCommand;
using RPS.Services.Game.Requests.Round.MakeChoice;

namespace RPS.Services.Game.Controllers;

[Authorize]
[ApiController]
[Route("api/rounds")]
public class RoundController(IMediator mediator, IClaimsProvider claimsProvider) : ControllerBase
{
    [HttpPost("make-choice")]
    public async Task MakeChoice([FromBody] MakeChoiceRequest request, CancellationToken cancellationToken)
    {
        var userId = claimsProvider.GetUserId(HttpContext.User);
        await mediator.SendAsync(new MakeChoiceCommand(request), cancellationToken);
    }
}