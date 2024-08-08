using Shared.Exceptions;

namespace Workout.Domain.Exception;

public class WorkoutPlanIsAlreadyAssignedToThisWardException(Guid id) : BaseException($"This workout plan with id {id} is already assigned to this ward ")
{
    public override string ErrorMessage =>"workout_plan_is_already_assigned_to_this_ward";
}