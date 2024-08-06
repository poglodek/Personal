using Microsoft.Extensions.DependencyInjection;
using User.Application.Service.SaltService;

namespace User.Application;

public static class ExtensionsApp
{
    public static IServiceCollection AddUserApplication(this IServiceCollection services)
    {
        services.AddScoped<ISaltService, SaltService>();
        
        return services;
    }
}