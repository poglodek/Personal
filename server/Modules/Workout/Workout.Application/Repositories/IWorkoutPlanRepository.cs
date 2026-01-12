using Workout.Domain.Entity;

namespace Workout.Application.Repositories;

public interface IWorkoutPlanRepository
{
    Task<WorkoutPlan?> GetWorkoutPlanByIdAsync(Guid requestWorkoutPlanId, CancellationToken cancellationToken);
    Task<WorkoutPlan?> GetWorkoutPlanByWardIdAndMonth(Guid wardId, int month, CancellationToken ct = default);
    Task<IEnumerable<WorkoutPlan>> GetByTrainerIdAsync(Guid trainerId, CancellationToken ct =default);
    Task AddWorkoutPlanAsync(WorkoutPlan workoutPlan, CancellationToken cancellationToken = default);
}