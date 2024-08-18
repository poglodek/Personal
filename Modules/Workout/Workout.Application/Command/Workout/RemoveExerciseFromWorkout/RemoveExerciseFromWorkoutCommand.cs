using MediatR;

namespace Workout.Application.Command.Workout.RemoveExerciseFromWorkout;

public record RemoveExerciseFromWorkoutCommand(Guid WorkoutId, Guid ExerciseId) : IRequest<Unit>;