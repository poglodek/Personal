namespace Workout.Application.Dto;

public record IdDto(Guid Id)
{
    public static implicit operator IdDto(Guid id) => new(id);
}