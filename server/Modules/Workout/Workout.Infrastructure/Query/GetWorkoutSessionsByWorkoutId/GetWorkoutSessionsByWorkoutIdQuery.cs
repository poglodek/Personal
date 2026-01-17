using MediatR;
using Workout.Application.Dto;

namespace Workout.Infrastructure.Query.GetWorkoutSessionsByWorkoutId;

public record GetWorkoutSessionsByWorkoutIdQuery(Guid WorkoutId) : IRequest<List<WorkoutSessionDto>>;
