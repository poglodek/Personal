using MediatR;
using Workout.Application.Dto;
using Workout.Application.Exception;
using Workout.Application.Repositories;

namespace Workout.Application.Command.Exercise.CopyExercise;

public class CopyExerciseCommandHandler : IRequestHandler<CopyExerciseCommand, ExerciseDto>
{
    private readonly IExerciseRepository _repository;

    public CopyExerciseCommandHandler(IExerciseRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ExerciseDto> Handle(CopyExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _repository.GetExerciseByIdAsync(request.Id, cancellationToken);
        if (exercise is null)
        {
            throw new ExerciseNotFound(request.Id);
        }
        
        var copy = exercise.Copy();
        
        await _repository.AddExerciseAsync(copy, cancellationToken);

        return copy.ToDto();
    }
}