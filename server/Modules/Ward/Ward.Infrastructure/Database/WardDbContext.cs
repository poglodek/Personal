using Dal.Postgres.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Ward.Infrastructure.Database;

public class WardDbContext : DbContext, IUnitOfWork
{
    public DbSet<Domain.Entity.Ward> Wards { get; init; }

    public WardDbContext(DbContextOptions<WardDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
    public ChangeTracker GetChangeTracker()
    {
        return ChangeTracker;
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return Database.BeginTransactionAsync(cancellationToken);
    }
}