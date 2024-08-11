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
 
    public Task<WorkoutPlan?> GetWorkoutPlanByWardIdAndMonth(Guid wardId, int month, CancellationToken ct = default)
    {
        return _dbContext.WorkoutPlans
            .TagWith("GetWorkoutPlanByWardIdAndMonth")
            .Where(x => x.WardId == wardId)
            .Where(x=> x.Active.Value)
            .Where(x => x.Workouts
                .Any(w => w.Dates
                    .Any(d => d.Value.Month == month)))
            .AsSplitQuery()
            .FirstOrDefaultAsync(ct);
    }

    public async Task<IEnumerable<WorkoutPlan>> GetByTrainerIdAsync(Guid trainerId, CancellationToken ct =default)
    {
        return await _dbContext.WorkoutPlans
            .TagWith("GetByTrainerIdAsync")
            .Where(x => x.TrainerId == trainerId)
            .Where(x => x.Active.Value)
            .AsSplitQuery()
            .ToListAsync(ct);


    }

    public async Task<IEnumerable<Exercise>> GetExercisesByTrainerIdAsync(Guid trainerId, CancellationToken cancellationToken)
    {
        return await _dbContext.Exercises
            .TagWith("GetExercisesByTrainerIdAsync")
            .IgnoreAutoIncludes()
            .Where(x => x.TrainerId == trainerId)
            .Where(x => x.Active.Value)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
        
    }

    
}