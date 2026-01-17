using Workout.Domain.ValueObject;

namespace Workout.Domain.Entity;

public class ExercisePerformance : Shared.Core.Entity
{
    public Guid ExerciseId { get; private set; }
    public Name ExerciseName { get; private set; }
    public Description ExerciseDescription { get; private set; }
    public int OrderIndex { get; private set; }
    public bool Completed { get; private set; }

    private readonly List<SetPerformance> _setPerformances = [];
    public IReadOnlyList<SetPerformance> SetPerformances => _setPerformances.AsReadOnly();

    private ExercisePerformance() { }

    public ExercisePerformance(
        Guid exerciseId,
        Name exerciseName,
        Description exerciseDescription,
        int orderIndex)
    {
        Id = Guid.NewGuid();
        ExerciseId = exerciseId;
        ExerciseName = exerciseName;
        ExerciseDescription = exerciseDescription;
        OrderIndex = orderIndex;
        Completed = false;
    }

    public void AddSetPerformance(SetPerformance setPerformance)
    {
        _setPerformances.Add(setPerformance);
    }

    public void MarkCompleted()
    {
        Completed = true;
    }

    public void MarkIncomplete()
    {
        Completed = false;
    }
}
