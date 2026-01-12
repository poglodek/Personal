using MediatR;
using Workout.Application.Dto;
using Workout.Application.Repositories;
using Workout.Domain.ValueObject;

namespace Workout.Application.Command.Exercise.AddExercise;

public class AddExerciseCommandHandler : IRequestHandler<AddExerciseCommand, ExerciseDto>
{
    private readonly IExerciseRepository _repository;

    public AddExerciseCommandHandler(IExerciseRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ExerciseDto> Handle(AddExerciseCommand request, CancellationToken cancellationToken)
    {
        var exercise = new Domain.Entity.Exercise(request.TrainerId, new Name(request.Name), new Description(request.Description), new Link(request.Link));
        
        await _repository.AddExerciseAsync(exercise, cancellationToken);

        return exercise.ToDto();
    }
}