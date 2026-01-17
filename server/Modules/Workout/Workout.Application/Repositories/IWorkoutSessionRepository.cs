using Workout.Domain.Entity;

namespace Workout.Application.Repositories;

public interface IWorkoutSessionRepository
{
    Task<WorkoutSession?> GetByIdAsync(Guid sessionId, CancellationToken ct = default);
    Task<IEnumerable<WorkoutSession>> GetByWardIdAsync(Guid wardId, DateOnly? startDate, DateOnly? endDate, CancellationToken ct = default);
    Task<IEnumerable<WorkoutSession>> GetByTrainerIdAsync(Guid trainerId, DateOnly? startDate, DateOnly? endDate, CancellationToken ct = default);
    Task<IEnumerable<WorkoutSession>> GetByWorkoutIdAsync(Guid workoutId, CancellationToken ct = default);
    Task AddAsync(WorkoutSession session, CancellationToken ct = default);
    Task<bool> ExistsForWorkoutAndDateAsync(Guid workoutId, DateOnly scheduledDate, CancellationToken ct = default);
}
