using Microsoft.AspNetCore.Mvc;
using RPS.Common.MediatR;
using RPS.Services.Auth.Features.Auth.Commands.LoginCommand;
using RPS.Services.Auth.Features.Auth.Commands.RegisterCommand;
using RPS.Services.Auth.Requests.Auth;
using RPS.Services.Auth.Requests.Auth.Login;
using RPS.Services.Auth.Requests.Auth.Register;

namespace RPS.Services.Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IMediatr mediatr) : ControllerBase
{
    [HttpPost("login")]
    public async Task<AuthResponse> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        return await mediatr.SendAsync<LoginCommand, AuthResponse>(new LoginCommand(request), cancellationToken);
    }

    [HttpPost("register")]
    public async Task<AuthResponse> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        return await mediatr.SendAsync<RegisterCommand, AuthResponse>(new RegisterCommand(request), cancellationToken);
    }
}