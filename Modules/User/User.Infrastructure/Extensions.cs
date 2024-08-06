using Dal.Postgres;
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
        
        return services;
    }
}