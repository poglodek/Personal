using MediatR;
using Workout.Infrastructure.Dto;

namespace Workout.Infrastructure.Query.GetTrainerExercise;

public record GetTrainerExerciseQuery(Guid TrainerId) : IRequest<List<ExerciseDto>>;