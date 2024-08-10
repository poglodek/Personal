using Shared.Exceptions;

namespace Workout.Infrastructure.Exception;

public class WorkOutPlanNotFound(Guid wardId, int month) : BaseException($"Workout plan for ward {wardId} in month {month} not found")
{
    public override string ErrorMessage => "workout_plan_not_found";
}