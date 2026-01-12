using System.Reflection;
using Messaging.Integration.Processor;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.Integration;

public static class Extensions
{
    public static IServiceCollection AddKafka(this IServiceCollection services)
    {
        services.AddTransient<IIntegrationProcessor, IIntegrationProcessor>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}