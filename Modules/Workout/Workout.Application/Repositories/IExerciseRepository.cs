using Workout.Domain.Entity;

namespace Workout.Application.Repositories;

public interface IExerciseRepository
{
    Task<Exercise?> GetExerciseById(Guid requestExerciseId, CancellationToken cancellationToken);
    Task<Exercise?> GetExerciseBySetId(Guid requestId, CancellationToken cancellationToken);
    Task AddExerciseAsync(Exercise exercise, CancellationToken cancellationToken);
}