using MediatR;
using Workout.Application.Exception;
using Workout.Application.Repositories;
using Workout.Domain.ValueObject;

namespace Workout.Application.Command.Exercise.EditExercise;

public class EditExerciseCommandHandler : IRequestHandler<EditExerciseCommand, Unit>
{
    private readonly IExerciseRepository _repository;

    public EditExerciseCommandHandler(IExerciseRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(EditExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = await _repository.GetExerciseById(request.Id, cancellationToken);
        
        if(exercise is null)
        {
            throw new ExerciseNotFound(request.Id);
        }
        
        exercise.SetNewDescription(new Description(request.Description));
        exercise.SetNewLink(new Link(request.Link));
        exercise.SetNewName(new Name(request.Name));
        
        if (request.Active)
        {
            exercise.DeActivate();
        }
        
        return Unit.Value;
    }
}