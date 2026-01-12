using MediatR;
using Workout.Application.Dto;

namespace Workout.Application.Command.Exercise.CopyExercise;

public record CopyExerciseCommand(Guid Id) : IRequest<ExerciseDto>;
