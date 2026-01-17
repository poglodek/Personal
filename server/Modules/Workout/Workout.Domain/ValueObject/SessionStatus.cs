namespace Workout.Domain.ValueObject;

public record SessionStatus
{
    public string Value { get; init; }

    private SessionStatus(string value)
    {
        Value = value;
    }

    public static SessionStatus Planned => new("Planned");
    public static SessionStatus InProgress => new("InProgress");
    public static SessionStatus Completed => new("Completed");
    public static SessionStatus Abandoned => new("Abandoned");

    public bool IsPlanned => Value == "Planned";
    public bool IsInProgress => Value == "InProgress";
    public bool IsCompleted => Value == "Completed";
    public bool IsAbandoned => Value == "Abandoned";
}
