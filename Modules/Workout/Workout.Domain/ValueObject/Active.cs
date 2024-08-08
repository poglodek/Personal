namespace Workout.Domain.ValueObject;

public record Active(bool Value)
{
    public static implicit operator Active(bool value) => new(value);
}