using ApiShared;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Notification.Shared;

public class NotificationModule : IModule
{
    public string ModuleName => "Notification";
    public IServiceCollection InstallModule(IServiceCollection services, IConfiguration configuration)
    {
        

        return services;
    }

    public IEndpointRouteBuilder AddEndPoints(IEndpointRouteBuilder endpointRoute)
    {
       
        return endpointRoute;
    }

    public IServiceProvider InstallModule(IServiceProvider serviceProvider)
    {
        
        
        return serviceProvider;
    }
}