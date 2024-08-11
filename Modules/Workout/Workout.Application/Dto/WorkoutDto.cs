namespace Workout.Application.Dto;

public record WorkoutDto(Guid Id, string Name, string Description, List<DateOnly> Dates, List<ExerciseDto> Exercises);



public static class WorkoutDtoMapper
{
    public static WorkoutDto MapToDto(this Domain.Entity.Workout workout)
    {
        return new WorkoutDto(workout.Id,
            workout.Name.Value,
            workout.Description.Value,
            workout.Dates.Select(x => x.Value).ToList(),
            workout.Exercises.Select(x => x.ToDto()).ToList());
    }
}