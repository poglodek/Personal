using MediatR;
using Workout.Application.Dto;

namespace Workout.Application.Command.Workout.CreateWorkout;

public record CreateWorkoutCommand(Guid WorkoutPlanId, string Name, string Description) : IRequest<WorkoutDto>;