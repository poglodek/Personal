using MediatR;
using Workout.Application.Dto;

namespace Workout.Application.Command.Exercise.AddSet;

public record CreateSetCommand(Guid ExerciseId, string Repeat, string Description, int RestTimeSeconds, string Type, RepetitionRateCommand RepetitionRate) : IRequest<SetDto>;
public record RepetitionRateCommand(string A, string B, string C, string D);