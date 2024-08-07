using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ward.Infrastructure.Database;

public class WardDbContextFactory : IDesignTimeDbContextFactory<WardDbContext>
{
    public WardDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<WardDbContext>();
        var connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=mysecretpassword";

        builder.UseNpgsql(connectionString);

        return new WardDbContext(builder.Options);
    }
}