using MediatR;
using Workout.Application.Dto;

namespace Workout.Infrastructure.Query.GetTrainerWorkoutPlans;

public record GetTrainerWorkoutPlansQuery(Guid TrainerId) : IRequest<IReadOnlyList<WorkoutPlanDto>>;