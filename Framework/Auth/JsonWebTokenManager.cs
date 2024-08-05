using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Funfair.Auth;
using Microsoft.IdentityModel.Tokens;

namespace Auth;

internal class JsonWebTokenManager(AuthOptions options, AuthExtensions.SecurityKeyCert key) : IJsonWebTokenManager
{
    private readonly SigningCredentials _signingCredentials = new SigningCredentials(key.Key, SecurityAlgorithms.HmacSha256);
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

    public JwtTokenDto CreateToken(Guid userId, string email, string role, IDictionary<string,string> claims = null)
    {
        if (claims is null)
        {
            claims = new Dictionary<string, string>(0);
        }

        var jwtClaims = new List<Claim>(claims.Count);


        if (claims.Any())
        {
            foreach (var claim in claims)
            {
                jwtClaims.Add(new Claim(claim.Key, claim.Value));
            }
        }
        
        jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
        jwtClaims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()));
        jwtClaims.Add(new Claim(ClaimTypes.Role, role));

        var expires = DateTime.Now.AddHours(options.ExpiresInHours);

        var jwt = new JwtSecurityToken(options.JwtIssuer, 
            options.JwtIssuer,
            claims: jwtClaims, 
            notBefore: DateTime.Now,
            expires: expires,
            signingCredentials: _signingCredentials);


        var token = _jwtSecurityTokenHandler.WriteToken(jwt);

        return new JwtTokenDto
        {
            JWT = token,
            ExpiresInHours = options.ExpiresInHours,
            UserId = userId,
            Role = role,
        };

    }
}