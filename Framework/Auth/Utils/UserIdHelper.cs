using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace Auth.Utils;

public static class UserIdHelper
{
    public static Guid GetUserId(this HttpContext context)
    {
        var userIdValue =  context.User.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;
        
        if(Guid.TryParse(userIdValue, out var userId))
        {
            return userId;
        }
        
        return Guid.Empty;
    }
}