using System.Security.Claims;
using RPS.Services.Auth.Domain.Entities;

namespace RPS.Services.Auth.Services.ClaimsProvider;

public class ClaimsManager : IClaimsManager
{
    public List<Claim> GetClaims(User user)
        =>
        [
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Role, user.Email)
        ];
}