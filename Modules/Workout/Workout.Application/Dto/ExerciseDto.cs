using Workout.Domain.Entity;

namespace Workout.Application.Dto;

public record ExerciseDto(Guid Id, Guid TrainerId, string Name, string Description, Guid? PrimaryId, bool Active, string Link,List<SetDto> Sets);

public static class ExerciseDtoExtensions
{
    public static ExerciseDto ToDto(this Exercise exercise)
    {
        return new ExerciseDto(exercise.Id,
            exercise.TrainerId,
            exercise.Name.Value,
            exercise.Description.Value,
            exercise.PrimaryId,
            exercise.Active.Value,
            exercise.Link.Value,
            exercise.Sets.Select(x=> x.ToDto()).ToList());
    }
}