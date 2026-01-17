namespace Workout.Domain.ValueObject;

public record SessionNotes(string Value)
{
    public SessionNotes() : this(string.Empty) { }
}
