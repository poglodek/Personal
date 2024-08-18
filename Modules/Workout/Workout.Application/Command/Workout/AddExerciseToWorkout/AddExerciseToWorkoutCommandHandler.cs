using MediatR;
using Workout.Application.Exception;
using Workout.Application.Repositories;

namespace Workout.Application.Command.Workout.AddExerciseToWorkout;

public class AddExerciseToWorkoutCommandHandler : IRequestHandler<AddExerciseToWorkoutCommand, Unit>
{
    private readonly IWorkoutRepository _workoutRepository;
    private readonly IExerciseRepository _exerciseRepository;

    public AddExerciseToWorkoutCommandHandler(IWorkoutRepository workoutRepository, IExerciseRepository exerciseRepository)
    {
        _workoutRepository = workoutRepository;
        _exerciseRepository = exerciseRepository;
    }
    
    public async Task<Unit> Handle(AddExerciseToWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = await _workoutRepository.GetWorkoutByIdAsync(request.WorkoutId, cancellationToken);
        var exercise = await _exerciseRepository.GetExerciseByIdAsync(request.ExerciseId, cancellationToken);

        if (workout is null)
            throw new WorkoutNotFound(request.WorkoutId);

        if (exercise is null)
            throw new ExerciseNotFound(request.ExerciseId);
        
        workout.AddExercise(exercise);
        
        return Unit.Value;
    }
}