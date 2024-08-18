using MediatR;
using Workout.Application.Dto;

namespace Workout.Application.Command.WorkoutPlan.CreateWorkoutPlan;

public record CreateWorkoutPlanCommand(Guid WardId, Guid TrainerId, string Name, string Description) : IRequest<WorkoutPlanDto>;