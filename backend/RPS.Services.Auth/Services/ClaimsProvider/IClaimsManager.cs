using System.Security.Claims;
using RPS.Services.Auth.Domain.Entities;

namespace RPS.Services.Auth.Services.ClaimsProvider;

public interface IClaimsManager
{
    List<Claim> GetClaims(User user);
    
}