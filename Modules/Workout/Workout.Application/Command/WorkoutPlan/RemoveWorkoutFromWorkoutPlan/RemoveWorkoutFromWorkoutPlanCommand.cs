using MediatR;

namespace Workout.Application.Command.WorkoutPlan.RemoveWorkoutFromWorkoutPlan;

public record RemoveWorkoutFromWorkoutPlanCommand(Guid WorkoutPlanId, Guid WorkoutId) : IRequest<Unit>;