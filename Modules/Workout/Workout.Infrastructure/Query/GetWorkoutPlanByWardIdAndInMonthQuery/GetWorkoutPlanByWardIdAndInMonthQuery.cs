using MediatR;
using Workout.Application.Dto;
using Workout.Domain.Entity;

namespace Workout.Infrastructure.Query.GetWorkoutPlanByWardIdAndInMonthQuery;

public record GetWorkoutPlanByWardIdAndInMonthQuery(Guid WardId, int Month) : IRequest<WorkoutPlanDto>;