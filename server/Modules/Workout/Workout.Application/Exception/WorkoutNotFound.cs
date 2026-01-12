using Shared.Exceptions;

namespace Workout.Application.Exception;

public class WorkoutNotFound(Guid id) : BaseException($"Workout with id {id} not found")
{
    public override string ErrorMessage => "workout_not_found";
}