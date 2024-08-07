using System.Reflection;
using Dal.Postgres;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ward.Application.Repositories;
using Ward.Infrastructure.Database;
using Ward.Infrastructure.Repositories;

namespace Ward.Infrastructure;

public static class ExtensionsInfra
{
    public static IServiceCollection AddWardInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase<WardDbContext>(configuration, "WardDb");
        services.AddScoped<IWardRepository, WardRepository>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }

    public static IApplicationBuilder UseInfra(this IApplicationBuilder application)
    {
        application.UseMigration<WardDbContext>();
        
        return application;
    }
}