using MediatR;
using Workout.Application.Dto;

namespace Workout.Application.Command.Exercise.EditExercise;

public record EditExerciseCommand(Guid Id, string Name, string Description, string Link, bool Active) : IRequest<Unit>;