using MediatR;
using Workout.Application.Exception;
using Workout.Application.Repositories;

namespace Workout.Application.Command.Exercise.RemoveSet;

public class RemoveSetCommandHandler : IRequestHandler<RemoveSetCommand, Unit>
{
    private readonly IExerciseRepository _repository;

    public RemoveSetCommandHandler(IExerciseRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(RemoveSetCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _repository.GetExerciseBySetId(request.Id, cancellationToken);
        if (exercise is null)
        {
            throw new SetInExerciseNotFound(request.Id);
        }
        
        exercise.RemoveSet(request.Id);
        
        
        return Unit.Value;
    }
}