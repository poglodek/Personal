namespace Workout.Domain.ValueObject;

public record RestTime(int Seconds)
{
    public static implicit operator RestTime(int value) => new(value);
}