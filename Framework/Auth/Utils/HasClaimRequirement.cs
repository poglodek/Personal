using Microsoft.AspNetCore.Authorization;

namespace Auth.Utils;

using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

public class HasClaimRequirement(string claimType) : IAuthorizationRequirement
{
    public string ClaimType { get; } = claimType;
}

internal class HasClaimHandler : AuthorizationHandler<HasClaimRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasClaimRequirement requirement)
    {
        var claim = context.User.Claims.FirstOrDefault(x=> x.Type == Const.Claims);
        
        if(claim?.Value is null)
        {
            context.Fail();
            return Task.CompletedTask;
        }
        
        var claims = claim!.Value!.Split(',');
        
        if(claims.Contains(requirement.ClaimType))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        

        return Task.CompletedTask;
    }
}