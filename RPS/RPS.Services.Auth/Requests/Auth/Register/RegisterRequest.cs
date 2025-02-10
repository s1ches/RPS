namespace RPS.Services.Auth.Requests.Auth.Register;

public class RegisterRequest
{
    public string UserName { get; set; } = null!;
    
    public string Email { get; set; } = null!;
    
    public string Password { get; set; } = null!;
    
    public string ConfirmPassword { get; set; } = null!;
}