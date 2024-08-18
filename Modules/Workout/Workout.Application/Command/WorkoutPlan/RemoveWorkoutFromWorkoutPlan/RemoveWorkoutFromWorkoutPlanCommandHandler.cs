using MediatR;
using Workout.Application.Exception;
using Workout.Application.Repositories;

namespace Workout.Application.Command.WorkoutPlan.RemoveWorkoutFromWorkoutPlan;

public class RemoveWorkoutFromWorkoutPlanCommandHandler : IRequestHandler<RemoveWorkoutFromWorkoutPlanCommand, Unit>
{
    private readonly IWorkoutPlanRepository _repository;

    public RemoveWorkoutFromWorkoutPlanCommandHandler(IWorkoutPlanRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(RemoveWorkoutFromWorkoutPlanCommand request, CancellationToken cancellationToken)
    {
        var workoutPlan = await _repository.GetWorkoutPlanByIdAsync(request.WorkoutPlanId, cancellationToken);
        
        if (workoutPlan is null)
        {
            throw new WorkoutPlanNotFound(request.WorkoutPlanId);
        }
        
        workoutPlan.RemoveWorkout(request.WorkoutId);

        return Unit.Value;
    }
}