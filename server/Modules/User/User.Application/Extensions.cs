using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Service.SaltService;

namespace User.Application;

public static class ExtensionsApp
{
    public static IServiceCollection AddUserApplication(this IServiceCollection services)
    {
        services.AddScoped<ISaltService, SaltService>();
        services.AddScoped<IPasswordHasher<User.Domain.Entity.User>, PasswordHasher<User.Domain.Entity.User>>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}