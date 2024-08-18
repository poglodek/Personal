using MediatR;

namespace Workout.Application.Command.Workout.EditWorkout;

public record EditWorkoutCommand(Guid WorkoutPlanId, string Name, string Description) : IRequest<Unit>;