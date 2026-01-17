using Workout.Domain.ValueObject;

namespace Workout.Domain.Entity;

public class SetPerformance : Shared.Core.Entity
{
    public Guid SetId { get; private set; }
    public int OrderIndex { get; private set; }

    // Planned values (snapshot from Set)
    public Repeat PlannedReps { get; private set; }
    public RestTime PlannedRestTime { get; private set; }
    public RepetitionRate? PlannedRepetitionRate { get; private set; }

    // Actual values (recorded during workout)
    public Repeat? ActualReps { get; private set; }
    public RestTime? ActualRestTime { get; private set; }
    public RepetitionRate? ActualRepetitionRate { get; private set; }
    public Weight? Weight { get; private set; }

    public bool Completed { get; private set; }
    public Description? Notes { get; private set; }

    private SetPerformance() { }

    public SetPerformance(
        Guid setId,
        int orderIndex,
        Repeat plannedReps,
        RestTime plannedRestTime,
        RepetitionRate? plannedRepetitionRate)
    {
        Id = Guid.NewGuid();
        SetId = setId;
        OrderIndex = orderIndex;
        PlannedReps = plannedReps;
        PlannedRestTime = plannedRestTime;
        PlannedRepetitionRate = plannedRepetitionRate;
        Completed = false;
    }

    public void RecordPerformance(
        Repeat? actualReps,
        RestTime? actualRestTime,
        Weight? weight,
        RepetitionRate? actualRepetitionRate,
        bool completed,
        Description? notes)
    {
        ActualReps = actualReps;
        ActualRestTime = actualRestTime;
        Weight = weight;
        ActualRepetitionRate = actualRepetitionRate;
        Completed = completed;
        Notes = notes;
    }

    public void UpdateNotes(Description notes)
    {
        Notes = notes;
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
