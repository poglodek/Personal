using Dal.Postgres.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace User.Infrastructure.Database;

internal class UserDbContext : DbContext, IUnitOfWork
{
    public DbSet<Domain.Entity.User> Users { get; init; }
    
    public ChangeTracker GetChangeTracker()
    {
        return ChangeTracker;
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return Database.BeginTransactionAsync(cancellationToken);
    }
}