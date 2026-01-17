using Workout.Domain.Entity;

namespace Workout.Application.Dto;

public record WorkoutSessionDto(
    Guid Id,
    Guid WorkoutId,
    Guid WorkoutPlanId,
    Guid WardId,
    Guid TrainerId,
    DateOnly ScheduledDate,
    DateTime? SessionDate,
    DateTime? StartTime,
    DateTime? EndTime,
    string Status,
    string Notes,
    WorkoutSnapshotDto WorkoutSnapshot,
    List<ExercisePerformanceDto> ExercisePerformances
);

public record WorkoutSnapshotDto(string Name, string Description);

public record ExercisePerformanceDto(
    Guid Id,
    Guid ExerciseId,
    string ExerciseName,
    string ExerciseDescription,
    int OrderIndex,
    bool Completed,
    List<SetPerformanceDto> SetPerformances
);

public record SetPerformanceDto(
    Guid Id,
    Guid SetId,
    int OrderIndex,
    string PlannedReps,
    int PlannedRestTimeSeconds,
    RepetitionRateDto? PlannedRepetitionRate,
    string? ActualReps,
    int? ActualRestTimeSeconds,
    RepetitionRateDto? ActualRepetitionRate,
    WeightDto? Weight,
    bool Completed,
    string? Notes
);

public record WeightDto(decimal Value, string Unit);

public static class WorkoutSessionDtoMapper
{
    public static WorkoutSessionDto MapToDto(this WorkoutSession session)
    {
        return new WorkoutSessionDto(
            session.Id,
            session.WorkoutId,
            session.WorkoutPlanId,
            session.WardId,
            session.TrainerId,
            session.ScheduledDate,
            session.SessionDate,
            session.StartTime,
            session.EndTime,
            session.Status.Value,
            session.Notes.Value,
            new WorkoutSnapshotDto(session.WorkoutSnapshot.Name, session.WorkoutSnapshot.Description),
            session.ExercisePerformances.Select(ep => ep.MapToDto()).ToList()
        );
    }

    public static ExercisePerformanceDto MapToDto(this ExercisePerformance exercisePerformance)
    {
        return new ExercisePerformanceDto(
            exercisePerformance.Id,
            exercisePerformance.ExerciseId,
            exercisePerformance.ExerciseName.Value,
            exercisePerformance.ExerciseDescription.Value,
            exercisePerformance.OrderIndex,
            exercisePerformance.Completed,
            exercisePerformance.SetPerformances.Select(sp => sp.MapToDto()).ToList()
        );
    }

    public static SetPerformanceDto MapToDto(this SetPerformance setPerformance)
    {
        return new SetPerformanceDto(
            setPerformance.Id,
            setPerformance.SetId,
            setPerformance.OrderIndex,
            setPerformance.PlannedReps.Value,
            setPerformance.PlannedRestTime.Seconds,
            setPerformance.PlannedRepetitionRate?.ToDto(),
            setPerformance.ActualReps?.Value,
            setPerformance.ActualRestTime?.Seconds,
            setPerformance.ActualRepetitionRate?.ToDto(),
            setPerformance.Weight != null
                ? new WeightDto(setPerformance.Weight.Value, setPerformance.Weight.Unit)
                : null,
            setPerformance.Completed,
            setPerformance.Notes?.Value
        );
    }
}
