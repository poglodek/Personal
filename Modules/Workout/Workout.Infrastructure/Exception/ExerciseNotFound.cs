using Shared.Exceptions;

namespace Workout.Infrastructure.Exception;

public class ExerciseNotFound(Guid id) : BaseException($"Exercise with id {id} not found")
{
    public override string ErrorMessage => "exercise_not_found";
}

