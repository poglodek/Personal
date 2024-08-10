namespace Workout.Infrastructure.Dto;

public record WorkoutPlanDto(Guid Id, Guid WardId, Guid TrainerId, string Name, string Description, bool Active, List<WorkoutDto> Workouts);

public static class WorkoutPlanDtoMapper
{
    public static WorkoutPlanDto MapToDto(this Domain.Entity.WorkoutPlan workoutPlan)
    {
        return new WorkoutPlanDto(workoutPlan.Id,
            workoutPlan.WardId,
            workoutPlan.TrainerId,
            workoutPlan.Name.Value,
            workoutPlan.Description.Value,
            workoutPlan.Active.Value,
            workoutPlan.Workouts.Select(x => x.MapToDto()).ToList());
    }
}