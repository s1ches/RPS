namespace RPS.Services.Auth.Requests.Auth.Login;

public class LoginRequest
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}