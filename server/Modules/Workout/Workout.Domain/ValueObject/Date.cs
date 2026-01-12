namespace Workout.Domain.ValueObject;

public record Date(DateOnly Value)
{
    public static implicit operator Date(DateOnly value) => new(value);
}