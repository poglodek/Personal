using MediatR;

namespace Workout.Application.Command.Workout.AddDateToWorkout;

public record AddDateToWorkoutCommand(Guid Id, DateOnly Date) : IRequest<Unit>;