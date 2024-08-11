using MediatR;
using Workout.Application.Dto;

namespace Workout.Application.Command.Exercise.AddExercise;

public record AddExerciseCommand(Guid TrainerId, string Name, string Description, string Link) : IRequest<ExerciseDto>;