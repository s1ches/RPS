using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RPS.Common.Options;
using RPS.Services.Auth.Domain.Entities;
using RPS.Services.Auth.Services.ClaimsProvider;

namespace RPS.Services.Auth.Services.TokenProvider;

public class TokenProvider(IClaimsManager claimsManager, IOptions<JwtOptions> jwtOptions) : ITokenProvider
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    
    public string GenerateToken(User user)
    {
        var userClaims = claimsManager.GetClaims(user);
        
        var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
        
        var jwt = new JwtSecurityToken(issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            expires: DateTime.Now.AddMinutes(_jwtOptions.AccessTokenLifetimeMinutes),
            claims: userClaims,
            signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);    
    }
}