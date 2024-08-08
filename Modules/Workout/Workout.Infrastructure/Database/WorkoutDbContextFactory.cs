using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Workout.Infrastructure.Database;

public class WorkoutDbContextFactory : IDesignTimeDbContextFactory<WorkoutDbContext>
{
    public WorkoutDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<WorkoutDbContext>();
        var connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=mysecretpassword";

        builder.UseNpgsql(connectionString);

        return new WorkoutDbContext(builder.Options);
    }
}