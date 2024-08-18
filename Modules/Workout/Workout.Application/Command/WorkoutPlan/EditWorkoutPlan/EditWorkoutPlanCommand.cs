using MediatR;

namespace Workout.Application.Command.WorkoutPlan.EditWorkoutPlan;

public record EditWorkoutPlanCommand(Guid WorkoutPlanId, string Name, string Description, bool Active) : IRequest<Unit>;