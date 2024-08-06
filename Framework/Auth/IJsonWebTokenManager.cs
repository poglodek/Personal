using Auth;

namespace Funfair.Auth;

public interface IJsonWebTokenManager
{
    JwtTokenDto CreateToken(Guid userId, string email, string role, List<string> claims = null);
}