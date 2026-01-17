using MediatR;
using Workout.Application.Dto;

namespace Workout.Infrastructure.Query.GetWorkoutSessionById;

public record GetWorkoutSessionByIdQuery(Guid SessionId) : IRequest<WorkoutSessionDto?>;
