using ApiShared;
using Auth.Utils;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ward.Application;
using Ward.Infrastructure;
using Ward.Infrastructure.Query.GetTrainer;
using Ward.Infrastructure.Query.GetTrainerWards;

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
        
        endpointRoute.MapGet("/GetTrainer/{wardId}", async (IMediator mediator, Guid wardId, CancellationToken ct = default) =>
        {
            var result = await mediator.Send(new GetTrainerQuery(wardId), ct);
            return result;
        }).RequireClaim("User")
          .CacheOutput();
        
        endpointRoute.MapGet("/GetWards/{trainerId}", async (IMediator mediator, Guid trainerId, CancellationToken ct = default) =>
        {
            var result = await mediator.Send(new GetTrainerWardsQuery(trainerId), ct);
            return result;
        }).RequireClaim("Ward")
          .CacheOutput();
        
        return endpointRoute;
    }

    public IServiceProvider InstallModule(IServiceProvider serviceProvider)
    {
        serviceProvider.UseInfra();
        
        return serviceProvider;
    }

    
}