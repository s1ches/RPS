using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPS.Common.MediatR;

namespace RPS.Services.Game.Controllers;

[Authorize]
[ApiController]
[Route("api/rounds")]
public class RoundController(IMediator mediator) : ControllerBase
{
    
}