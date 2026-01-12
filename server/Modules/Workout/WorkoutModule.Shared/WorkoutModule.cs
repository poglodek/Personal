using ApiShared;

using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workout.Infrastructure;

namespace WorkoutModule.Shared;

public class WorkoutModule : IModule
{
    public string ModuleName => "Workout";
    public IServiceCollection InstallModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddWorkoutInfra(configuration);

        return services;
    }

    public IEndpointRouteBuilder AddEndPoints(IEndpointRouteBuilder endpointRoute)
    {
       
        return endpointRoute;
    }

    public IServiceProvider InstallModule(IServiceProvider serviceProvider)
    {
        serviceProvider.UseWorkoutInfra();
        
        return serviceProvider;
    }
}