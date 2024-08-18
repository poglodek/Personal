using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Workout.Application.Repositories;
using Workout.Domain.Entity;
using Workout.Infrastructure.Database;

namespace Workout.Infrastructure.Repositories;

internal class WorkoutPlanRepository : IWorkoutPlanRepository
{
    private readonly WorkoutDbContext _dbContext;
    
    public WorkoutPlanRepository(WorkoutDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<WorkoutPlan?> GetWorkoutPlanByIdAsync(Guid workoutPlanId, CancellationToken cancellationToken)
    {
        return _dbContext.WorkoutPlans.FirstOrDefaultAsync(x=> x.Id == workoutPlanId, cancellationToken);
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

    public async Task AddWorkoutPlanAsync(WorkoutPlan workoutPlan, CancellationToken cancellationToken = default)
    {
        await _dbContext.WorkoutPlans.AddAsync(workoutPlan, cancellationToken);
    }
}