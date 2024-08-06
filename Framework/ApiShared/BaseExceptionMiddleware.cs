using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Exceptions;

namespace ApiShared;

internal class BaseExceptionMiddleware(ILogger<BaseExceptionMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (BaseException ex)
        {
            logger.LogWarning($"Get exception: {ex.Message}");
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(ex.Message);
            
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Internal server error");
            
        }
    }
}