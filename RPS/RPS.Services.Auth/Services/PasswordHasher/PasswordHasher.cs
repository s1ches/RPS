namespace RPS.Services.Auth.Services.PasswordHasher;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);
    
    public bool IsCorrectPassword(string password, string hash)
        => BCrypt.Net.BCrypt.Verify(text: password, hash: hash);
}