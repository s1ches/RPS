using RPS.Services.Auth.Domain.Entities;

namespace RPS.Services.Auth.Services.TokenProvider;

public interface ITokenProvider
{
    public string GenerateToken(User user);
}