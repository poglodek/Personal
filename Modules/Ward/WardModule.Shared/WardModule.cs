using ApiShared;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WardModule.Shared;

public class WardModule : IModule
{
    public string ModuleName => "Ward";
    public IServiceCollection InstallModule(IServiceCollection services, IConfiguration configuration)
    {
       

        return services;
    }

    public IEndpointRouteBuilder AddEndPoints(IEndpointRouteBuilder endpointRoute)
    {
        return endpointRoute;
    }

    public IApplicationBuilder InstallModule(IApplicationBuilder app)
    {
        return app;
    }

    
}