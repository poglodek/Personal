using MediatR;

namespace Workout.Application.Command.WorkoutSession.RecordSetPerformance;

public record RecordSetPerformanceCommand(
    Guid SessionId,
    Guid SetPerformanceId,
    string? ActualReps,
    int? ActualRestTimeSeconds,
    decimal? WeightValue,
    string? WeightUnit,
    string? ActualRepetitionRateA,
    string? ActualRepetitionRateB,
    string? ActualRepetitionRateC,
    string? ActualRepetitionRateD,
    bool Completed,
    string? Notes
) : IRequest;
