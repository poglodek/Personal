using Workout.Domain.Entity;

namespace Workout.Application.Repositories;

public interface IWorkoutRepository
{
    Task<WorkoutPlan?> GetWorkoutPlanByWardIdAndMonth(Guid wardId, int month, CancellationToken ct = default);
    Task<IEnumerable<WorkoutPlan>> GetByTrainerIdAsync(Guid trainerId, CancellationToken ct =default);
    Task<IEnumerable<Exercise>> GetExercisesByTrainerIdAsync(Guid trainerId, CancellationToken cancellationToken);
}