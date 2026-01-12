using MediatR;
using Workout.Application.Dto;

namespace Workout.Infrastructure.Query.GetTrainerExercise;

public record GetTrainerExerciseQuery(Guid TrainerId) : IRequest<List<ExerciseDto>>;