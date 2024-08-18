using MediatR;
using Workout.Application.Exception;
using Workout.Application.Repositories;

namespace Workout.Application.Command.Workout.RemoveExerciseFromWorkout;

public class RemoveExerciseFromWorkoutCommandHandler : IRequestHandler<RemoveExerciseFromWorkoutCommand,Unit>
{
    private readonly IWorkoutRepository _repository;

    public RemoveExerciseFromWorkoutCommandHandler(IWorkoutRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(RemoveExerciseFromWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = await _repository.GetWorkoutByIdAsync(request.WorkoutId, cancellationToken);
        if (workout is null)
        {
            throw new WorkoutNotFound(request.WorkoutId);
        }
        
        workout.RemoveExercise(request.ExerciseId);
        
        return Unit.Value;
    }
}