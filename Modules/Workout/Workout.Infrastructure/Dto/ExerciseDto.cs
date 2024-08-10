using Workout.Domain.Entity;

namespace Workout.Infrastructure.Dto;

public record ExerciseDto(Guid Id, Guid TrainerId, string Name, string Description, Guid? PrimaryId, bool Active, List<SetDto> Sets);

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
            exercise.Sets.Select(x=> x.ToDto()).ToList());
    }
}