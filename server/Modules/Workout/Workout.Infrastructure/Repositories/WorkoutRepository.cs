using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Workout.Application.Repositories;
using Workout.Domain.Entity;
using Workout.Infrastructure.Database;

namespace Workout.Infrastructure.Repositories;

internal class WorkoutRepository : IWorkoutRepository
{
    private readonly WorkoutDbContext _dbContext;
    
    public WorkoutRepository(WorkoutDbContext dbContext, IMemoryCache cache)
    {
        _dbContext = dbContext;
    }


    public Task<Domain.Entity.Workout?> GetWorkoutByIdAsync(Guid requestId, CancellationToken cancellationToken)
    {
        return _dbContext.Workouts.FirstOrDefaultAsync(x=> x.Id == requestId, cancellationToken);
    }
}