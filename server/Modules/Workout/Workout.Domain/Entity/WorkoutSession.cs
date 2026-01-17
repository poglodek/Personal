using Workout.Domain.ValueObject;

namespace Workout.Domain.Entity;

public class WorkoutSession : Shared.Core.Entity
{
    public Guid WorkoutId { get; private set; }
    public Guid WorkoutPlanId { get; private set; }
    public Guid WardId { get; private set; }
    public Guid TrainerId { get; private set; }

    public DateOnly ScheduledDate { get; private set; }
    public DateTime? SessionDate { get; private set; }
    public DateTime? StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }

    public SessionStatus Status { get; private set; }
    public SessionNotes Notes { get; private set; }
    public WorkoutSnapshot WorkoutSnapshot { get; private set; }

    private readonly List<ExercisePerformance> _exercisePerformances = [];
    public IReadOnlyList<ExercisePerformance> ExercisePerformances => _exercisePerformances.AsReadOnly();

    private WorkoutSession() { }

    public WorkoutSession(
        Guid workoutId,
        Guid workoutPlanId,
        Guid wardId,
        Guid trainerId,
        DateOnly scheduledDate,
        WorkoutSnapshot workoutSnapshot)
    {
        Id = Guid.NewGuid();
        WorkoutId = workoutId;
        WorkoutPlanId = workoutPlanId;
        WardId = wardId;
        TrainerId = trainerId;
        ScheduledDate = scheduledDate;
        WorkoutSnapshot = workoutSnapshot;
        Status = SessionStatus.Planned;
        Notes = new SessionNotes();
    }

    public void StartSession()
    {
        if (Status.IsInProgress)
        {
            return; // Already started
        }

        Status = SessionStatus.InProgress;
        StartTime = DateTime.UtcNow;
        SessionDate = DateTime.UtcNow;
    }

    public void CompleteSession()
    {
        if (Status.IsCompleted)
        {
            return; // Already completed
        }

        Status = SessionStatus.Completed;
        EndTime = DateTime.UtcNow;

        if (SessionDate == null)
        {
            SessionDate = DateTime.UtcNow;
        }
    }

    public void AbandonSession()
    {
        Status = SessionStatus.Abandoned;
        EndTime = DateTime.UtcNow;

        if (SessionDate == null)
        {
            SessionDate = DateTime.UtcNow;
        }
    }

    public void AddExercisePerformance(ExercisePerformance exercisePerformance)
    {
        _exercisePerformances.Add(exercisePerformance);
    }

    public void UpdateNotes(SessionNotes notes)
    {
        Notes = notes;
    }
}
