using System.Security.Claims;

namespace Auth;

public interface IJsonWebTokenManager
{
    JwtTokenDto CreateToken(Guid userId, string email, string role);
    string GenerateRefreshToken();
    string HashRefreshToken(string refreshToken);
}