using Shared.Exceptions;

namespace Workout.Application.Exception;

public class WorkoutPlanNotFound(Guid  id) : BaseException($"WorkoutPlan with id {id} not found")
{
    public override string ErrorMessage => "workout_plan_not_found";
}