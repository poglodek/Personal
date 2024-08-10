using MediatR;
using Workout.Domain.Entity;
using Workout.Infrastructure.Dto;

namespace Workout.Infrastructure.Query.GetWorkoutPlanByWardIdAndInMonthQuery;

public record GetWorkoutPlanByWardIdAndInMonthQuery(Guid WardId, int Month) : IRequest<WorkoutPlanDto>;