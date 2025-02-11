using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPS.Common.MediatR;

namespace RPS.Services.Game.Controllers;

[Authorize]
[ApiController]
[Route("api/games")]
public class GameController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task JoinGame()
    {
        
    }
}