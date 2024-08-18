using MediatR;
using Workout.Application.Exception;
using Workout.Application.Repositories;

namespace Workout.Application.Command.WorkoutPlan.AddWorkoutToWorkoutPlan;

public class AddWorkoutToWorkoutPlanCommandHandler : IRequestHandler<AddWorkoutToWorkoutPlanCommand, Unit>
{
    private readonly IWorkoutPlanRepository _workoutPlanRepository;
    private readonly IWorkoutRepository _workoutRepository;

    public AddWorkoutToWorkoutPlanCommandHandler(IWorkoutPlanRepository workoutPlanRepository, IWorkoutRepository workoutRepository)
    {
        _workoutPlanRepository = workoutPlanRepository;
        _workoutRepository = workoutRepository;
    }
    
    public async Task<Unit> Handle(AddWorkoutToWorkoutPlanCommand request, CancellationToken cancellationToken)
    {
        var workoutPlan = await _workoutPlanRepository.GetWorkoutPlanByIdAsync(request.WorkoutPlanId, cancellationToken);
        if (workoutPlan is null)
        {
            throw new WorkoutPlanNotFound(request.WorkoutPlanId);
        }
        
        var workout = await _workoutRepository.GetWorkoutByIdAsync(request.WorkoutId, cancellationToken);
        if (workout is null)
        {
            throw new WorkoutNotFound(request.WorkoutId);
        }
        
        workoutPlan.AddWorkout(workout);
        
        return Unit.Value;
    }
}