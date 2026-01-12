using MediatR;
using Workout.Application.Exception;
using Workout.Application.Repositories;
using Workout.Domain.ValueObject;

namespace Workout.Application.Command.Workout.RemoveDateFromWorkout;

public class RemoveDateFromWorkoutCommandHandler : IRequestHandler<RemoveDateFromWorkoutCommand,Unit>
{
    private readonly IWorkoutRepository _repository;

    public RemoveDateFromWorkoutCommandHandler(IWorkoutRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(RemoveDateFromWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = await _repository.GetWorkoutByIdAsync(request.WorkoutId, cancellationToken);
        if (workout is null)
        {
            throw new WorkoutNotFound(request.WorkoutId);
        }
        
        workout.RemoveDate(DateOnly.FromDateTime(request.Date));


        return Unit.Value;
    }
}