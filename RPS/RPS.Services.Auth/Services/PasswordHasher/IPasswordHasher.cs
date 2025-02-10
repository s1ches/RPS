namespace RPS.Services.Auth.Services.PasswordHasher;

public interface IPasswordHasher
{
    string HashPassword(string password);

    bool IsCorrectPassword(string password, string hash);
}