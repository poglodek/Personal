using MediatR;

namespace Workout.Application.Command.Workout.RemoveDateFromWorkout;

public record RemoveDateFromWorkoutCommand(Guid WorkoutId, DateTime Date) : IRequest<Unit>;