using Microsoft.EntityFrameworkCore;
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
    public Task<WorkoutPlan?> GetWorkoutPlanByWardIdAndMonth(Guid wardId, int month, CancellationToken ct = default)
    {
        return _dbContext.WorkoutPlans
            .TagWith("GetWorkoutPlanByWardIdAndMonth")
            .Where(x => x.WardId == wardId)
            .Where(x => x.Workouts
                .Any(w => w.Dates
                    .Any(d => d.Value.Month == month)))
            .AsSplitQuery()
            .FirstOrDefaultAsync(ct);
    }
}