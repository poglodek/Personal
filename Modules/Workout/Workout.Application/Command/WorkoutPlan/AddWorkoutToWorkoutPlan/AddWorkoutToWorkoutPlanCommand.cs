using MediatR;

namespace Workout.Application.Command.WorkoutPlan.AddWorkoutToWorkoutPlan;

public record AddWorkoutToWorkoutPlanCommand(Guid WorkoutId, Guid WorkoutPlanId) : IRequest<Unit>;