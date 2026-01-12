using Shared.Exceptions;

namespace Workout.Application.Exception;

public class SetInExerciseNotFound(Guid id) : BaseException($"Set in exercise with id {id} not found")
{
    public override string ErrorMessage => "set_in_exercise_not_found";
}