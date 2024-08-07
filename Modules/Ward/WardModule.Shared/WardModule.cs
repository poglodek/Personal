using ApiShared;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ward.Application;
using Ward.Infrastructure;

namespace WardModule.Shared;

public class WardModule : IModule
{
    public string ModuleName => "Ward";
    public IServiceCollection InstallModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddWardApp(configuration);
        services.AddWardInfra(configuration);

        return services;
    }

    public IEndpointRouteBuilder AddEndPoints(IEndpointRouteBuilder endpointRoute)
    {
        endpointRoute.MapGet("/", () => "ward");
        return endpointRoute;
    }

    public IApplicationBuilder InstallModule(IApplicationBuilder app)
    {
        app.UseInfra();
        return app;
    }

    
}