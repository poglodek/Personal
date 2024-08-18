using MediatR;
using Workout.Application.Dto;

namespace Workout.Application.Command.Workout.AddExerciseToWorkout;

public record AddExerciseToWorkoutCommand(Guid WorkoutId, Guid ExerciseId) : IRequest<Unit>;