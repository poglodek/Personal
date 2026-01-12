namespace Workout.Domain.ValueObject;

public record Type(string Value)
{
    public static implicit operator Type(string value) => new(value);
}