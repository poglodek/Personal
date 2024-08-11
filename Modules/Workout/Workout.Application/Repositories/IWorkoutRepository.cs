using Workout.Domain.Entity;

namespace Workout.Application.Repositories;

public interface IWorkoutRepository
{
    Task<Domain.Entity.Workout?> GetWorkoutByIdAsync(Guid requestId, CancellationToken cancellationToken);
}