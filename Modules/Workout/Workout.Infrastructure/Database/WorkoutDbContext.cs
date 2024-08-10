using Dal.Postgres.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Workout.Domain.Entity;
using Workout.Domain.ValueObject;

namespace Workout.Infrastructure.Database;

public class WorkoutDbContext : DbContext, IUnitOfWork
{
    public DbSet<WorkoutPlan> WorkoutPlans { get; init; }
    public DbSet<Set> Sets { get;  init; }
    public DbSet<Domain.Entity.Workout> Workouts { get;  init; }
    public DbSet<Exercise> Exercises { get;  init; }

    public WorkoutDbContext(DbContextOptions<WorkoutDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        
        modelBuilder.Entity<Date>().HasNoKey();
        
        modelBuilder.Entity<WorkoutPlan>()
            .Navigation(b => b.Workouts)
            .AutoInclude();
        
        modelBuilder.Entity<Domain.Entity.Workout>()
            .Navigation(b => b.Exercises)
            .AutoInclude();
        
          
        modelBuilder.Entity<Exercise>()
            .Navigation(b => b.Sets)
            .AutoInclude();
        
        
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