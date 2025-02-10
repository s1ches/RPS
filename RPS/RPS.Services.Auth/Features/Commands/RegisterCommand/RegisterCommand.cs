using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Services.Auth.Requests.Auth;
using RPS.Services.Auth.Requests.Auth.Register;

namespace RPS.Services.Auth.Features.Commands.RegisterCommand;

public class RegisterCommand : IRequest<AuthResponse>
{
    public RegisterCommand(RegisterRequest registerRequest)
    {
        UserName = registerRequest.UserName;
        Password = registerRequest.Password;
        Email = registerRequest.Email;
    }
    
    public string UserName { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
}