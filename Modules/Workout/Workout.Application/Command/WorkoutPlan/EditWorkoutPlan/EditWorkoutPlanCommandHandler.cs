using MediatR;
using Workout.Application.Exception;
using Workout.Application.Repositories;
using Workout.Domain.ValueObject;

namespace Workout.Application.Command.WorkoutPlan.EditWorkoutPlan;

public class EditWorkoutPlanCommandHandler : IRequestHandler<EditWorkoutPlanCommand, Unit>
{
    private readonly IWorkoutPlanRepository _repository;

    public EditWorkoutPlanCommandHandler(IWorkoutPlanRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(EditWorkoutPlanCommand request, CancellationToken cancellationToken)
    {
        var workoutPlan = await _repository.GetWorkoutPlanByIdAsync(request.WorkoutPlanId, cancellationToken);

        if (workoutPlan is null)
        {
            throw new WorkoutPlanNotFound(request.WorkoutPlanId);
        }
        
        workoutPlan.SetNewName(new Name(request.Name));
        workoutPlan.SetNewDescription(new Description(request.Description));
        
        if (!request.Active)
        {
            workoutPlan.DeActivate();
        }
        
        return Unit.Value;
    }
}