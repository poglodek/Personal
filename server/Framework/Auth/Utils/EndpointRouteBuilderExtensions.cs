using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;

namespace Auth.Utils;

public static class EndpointRouteBuilderExtensions
{
    public static RouteHandlerBuilder RequireClaim(this RouteHandlerBuilder builder, string claimType)
    {
        builder.Add(endpointBuilder =>
        {
            var metadata = endpointBuilder.Metadata;
            metadata.Add(new AuthorizeAttribute());
            metadata.Add(new HasClaimRequirement(claimType));
        });
        
        return builder;
    }
}