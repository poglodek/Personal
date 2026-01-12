using Workout.Domain.Entity;

namespace Workout.Application.Repositories;

public interface IExerciseRepository
{
    Task<Exercise?> GetExerciseByIdAsync(Guid requestExerciseId, CancellationToken cancellationToken);
    Task<Exercise?> GetExerciseBySetId(Guid requestId, CancellationToken cancellationToken);
    Task AddExerciseAsync(Exercise exercise, CancellationToken cancellationToken);
    Task<IEnumerable<Exercise>> GetExercisesByTrainerIdAsync(Guid trainerId, CancellationToken cancellationToken);
}