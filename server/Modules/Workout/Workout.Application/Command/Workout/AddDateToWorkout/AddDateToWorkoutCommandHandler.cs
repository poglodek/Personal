using MediatR;
using Workout.Application.Exception;
using Workout.Application.Repositories;
using Workout.Domain.ValueObject;

namespace Workout.Application.Command.Workout.AddDateToWorkout;

public class AddDateToWorkoutCommandHandler : IRequestHandler<AddDateToWorkoutCommand,Unit>
{
    private readonly IWorkoutRepository _repository;

    public AddDateToWorkoutCommandHandler(IWorkoutRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(AddDateToWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = await _repository.GetWorkoutByIdAsync(request.Id, cancellationToken);
        if (workout is null)
        {
            throw new WorkoutNotFound(request.Id);
        }
        
        workout.AddDate(new Date(request.Date));

        return Unit.Value;
    }
}