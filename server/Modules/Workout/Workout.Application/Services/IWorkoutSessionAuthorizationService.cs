namespace Workout.Application.Services;

public interface IWorkoutSessionAuthorizationService
{
    Task<bool> UserCanAccessSession(Guid userId, Guid sessionId, CancellationToken ct = default);
    Task<bool> UserCanAccessWorkout(Guid userId, Guid workoutId, CancellationToken ct = default);
}
