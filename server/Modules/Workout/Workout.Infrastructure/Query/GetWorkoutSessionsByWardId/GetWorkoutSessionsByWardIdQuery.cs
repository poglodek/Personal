using MediatR;
using Workout.Application.Dto;

namespace Workout.Infrastructure.Query.GetWorkoutSessionsByWardId;

public record GetWorkoutSessionsByWardIdQuery(Guid WardId, DateOnly? StartDate, DateOnly? EndDate) : IRequest<List<WorkoutSessionDto>>;
