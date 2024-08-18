using System.Reflection;
using Dal.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workout.Application.Repositories;
using Workout.Infrastructure.Database;
using Workout.Infrastructure.Repositories;

namespace Workout.Infrastructure;

public static class ExtensionsInfra
{
    public static IServiceCollection AddWorkoutInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase<WorkoutDbContext>(configuration, "WorkoutDb");
        services.AddScoped<IWorkoutPlanRepository, WorkoutPlanRepository>();
        services.AddScoped<IWorkoutRepository, WorkoutRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }

    public static IServiceProvider UseWorkoutInfra(this IServiceProvider serviceProvider)
    {
        serviceProvider.UseMigration<WorkoutDbContext>();
        
        return serviceProvider;
    }
}