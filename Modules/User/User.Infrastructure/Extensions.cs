using System.Reflection;
using Dal.Postgres;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Repositories;
using User.Infrastructure.Database;
using User.Infrastructure.Repositories;

namespace User.Infrastructure;

public static class ExtensionsInfra
{
    public static IServiceCollection AddUserInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase<UserDbContext>(configuration, "UsersDb");
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }

    public static IApplicationBuilder UseInfra(this IApplicationBuilder application)
    {
        application.UseMigration<UserDbContext>();
        
        return application;
    }
}