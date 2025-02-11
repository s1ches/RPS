using System.Net;
using System.Security.Claims;
using RPS.Common.Exceptions;

namespace RPS.Common.Services.ClaimsProvider;

public class ClaimsProvider : IClaimsProvider
{
    public long GetUserId(ClaimsPrincipal claims)
    {
        var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            throw new ApplicationExceptionBase($"Invalid claims of user {claims.FindFirstValue(ClaimTypes.Email)}",
                HttpStatusCode.Unauthorized);

        return long.Parse(userId);
    }

    public string GetUserName(ClaimsPrincipal claims)
    {
        var userName = claims.FindFirstValue(ClaimTypes.Name);

        if (userName is null)
            throw new ApplicationExceptionBase($"Invalid claims of user {claims.FindFirstValue(ClaimTypes.Email)}",
                HttpStatusCode.Unauthorized);

        return userName;    
    }

    public string GetEmail(ClaimsPrincipal claims)
    {
        var email = claims.FindFirstValue(ClaimTypes.Email);

        if (email is null)
            throw new ApplicationExceptionBase($"Invalid claims of user {claims.FindFirstValue(ClaimTypes.Email)}",
                HttpStatusCode.Unauthorized);

        return email;        
    }
}