using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Services.Auth.Requests.Auth;
using RPS.Services.Auth.Requests.Auth.Register;

namespace RPS.Services.Auth.Features.Auth.Commands.RegisterCommand;

public class RegisterCommand : IRequest<AuthResponse>
{
    public RegisterCommand(RegisterRequest request)
    {
        UserName = request.UserName;
        Password = request.Password;
        Email = request.Email;
        ConfirmPassword = request.ConfirmPassword;
    }
    
    public string ConfirmPassword { get; set; }
    
    public string UserName { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
}