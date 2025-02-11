using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Services.Auth.Requests.Auth;
using RPS.Services.Auth.Requests.Auth.Login;

namespace RPS.Services.Auth.Features.Auth.Commands.LoginCommand;

public class LoginCommand : IRequest<AuthResponse>
{ 
    public LoginCommand(LoginRequest request)
    {
        Email = request.Email;
        Password = request.Password;
    }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
}