using MediatR;
using Workout.Application.Dto;

namespace Workout.Infrastructure.Query.GetWorkoutSessionsByTrainerId;

public record GetWorkoutSessionsByTrainerIdQuery(Guid TrainerId, DateOnly? StartDate, DateOnly? EndDate) : IRequest<List<WorkoutSessionDto>>;
