using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Funfair.Auth;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Auth;

internal class JsonWebTokenManager(AuthOptions options, AuthExtensions.SecurityKeyCert key) : IJsonWebTokenManager
{
    private readonly SigningCredentials _signingCredentials = new (key.Key, SecurityAlgorithms.HmacSha256);
    
    public JwtTokenDto CreateToken(Guid userId, string email, string role)
    {
        List<Claim> jwtClaims = [
            new (JwtRegisteredClaimNames.Email, email),
            new (ClaimTypes.Role, role),
            new (JwtRegisteredClaimNames.UniqueName, userId.ToString())
        ];
        
        var expires = DateTime.Now.AddHours(options.ExpiresInHours);
        
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(jwtClaims),
            Expires = expires,
            SigningCredentials = _signingCredentials,
            Issuer = options.JwtIssuer,
            Audience = options.JwtAudience,
        };

        var tokenHandler = new JsonWebTokenHandler();

        var accessToken = tokenHandler.CreateToken(tokenDescriptor);

        return new JwtTokenDto
        {
            AccessToken = accessToken,
            ExpiresAt = expires,
            UserId = userId,
            Role = role,
        };
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public string HashRefreshToken(string refreshToken)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken));
        return Convert.ToBase64String(bytes);
    }
}