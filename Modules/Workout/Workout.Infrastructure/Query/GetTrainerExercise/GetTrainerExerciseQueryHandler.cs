using MediatR;
using Workout.Application.Dto;
using Workout.Application.Repositories;

namespace Workout.Infrastructure.Query.GetTrainerExercise;

public class GetTrainerExerciseQueryHandler : IRequestHandler<GetTrainerExerciseQuery, List<ExerciseDto>>
{
    private readonly IWorkoutRepository _repository;

    public GetTrainerExerciseQueryHandler(IWorkoutRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<ExerciseDto>> Handle(GetTrainerExerciseQuery request, CancellationToken cancellationToken)
    {
        var exercises = await _repository.GetExercisesByTrainerIdAsync(request.TrainerId, cancellationToken);
        
        return exercises.Select(x => x.ToDto()).ToList();
    }
}