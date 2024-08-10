using Workout.Domain.Entity;

namespace Workout.Application.Repositories;

public interface IWorkoutPlanRepository
{
    Task<WorkoutPlan?> GetWorkoutPlanByWardIdAndMonth(Guid requestWardId, int month, CancellationToken ct = default);
}