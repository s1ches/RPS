using System.Security.Claims;

namespace RPS.Common.Services.ClaimsProvider;

public interface IClaimsProvider
{
    long GetUserId(ClaimsPrincipal claims);
    
    string GetUserName(ClaimsPrincipal claims);
    
    string GetEmail(ClaimsPrincipal claims);
}