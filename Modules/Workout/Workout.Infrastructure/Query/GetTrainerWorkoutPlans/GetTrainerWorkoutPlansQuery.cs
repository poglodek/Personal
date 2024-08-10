using MediatR;
using Workout.Infrastructure.Dto;

namespace Workout.Infrastructure.Query.GetTrainerWorkoutPlans;

public record GetTrainerWorkoutPlansQuery(Guid TrainerId) : IRequest<IReadOnlyList<WorkoutPlanDto>>;