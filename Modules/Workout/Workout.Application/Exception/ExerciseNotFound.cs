using Shared.Exceptions;

namespace Workout.Application.Exception;

public class ExerciseNotFound(Guid  id) : BaseException($"Exercise with id {id} not found")
{
    public override string ErrorMessage => "exercise_not_found";
}