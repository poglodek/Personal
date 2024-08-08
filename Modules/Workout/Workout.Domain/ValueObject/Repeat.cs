namespace Workout.Domain.ValueObject;

public record Repeat(string Value)
{
    public static implicit operator Repeat(string value) => new(value);
}