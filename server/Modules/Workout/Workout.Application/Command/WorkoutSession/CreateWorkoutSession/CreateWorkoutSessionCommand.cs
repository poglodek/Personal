using MediatR;
using Workout.Application.Dto;

namespace Workout.Application.Command.WorkoutSession.CreateWorkoutSession;

public record CreateWorkoutSessionCommand(
    Guid WorkoutPlanId,
    Guid WorkoutId,
    DateOnly ScheduledDate
) : IRequest<WorkoutSessionDto>;
