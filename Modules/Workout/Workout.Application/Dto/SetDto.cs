using Workout.Domain.Entity;
using Workout.Domain.ValueObject;

namespace Workout.Application.Dto;

public record SetDto(Guid Id, string Repeat, string Description, int RestTimeSeconds, string Type, RepetitionRateDto RepetitionRate);

public record RepetitionRateDto(string A, string B, string C, string D);

public static class RepetitionRateDtoMapper
{
    public static RepetitionRateDto ToDto(this RepetitionRate repetitionRate)
    {
        return new RepetitionRateDto(repetitionRate.A, repetitionRate.B, repetitionRate.C, repetitionRate.D);
    }
}

public static class SetDtoMapper
{
    public static SetDto ToDto(this Set set)
    {
        return new SetDto(set.Id, set.Repeat.Value, set.Description.Value, set.RestTime.Seconds, set.Type.Value, set.RepetitionRate.ToDto());
    }
}