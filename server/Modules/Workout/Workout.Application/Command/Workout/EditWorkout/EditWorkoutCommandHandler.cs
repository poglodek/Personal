using MediatR;
using Workout.Application.Exception;
using Workout.Application.Repositories;
using Workout.Domain.ValueObject;

namespace Workout.Application.Command.Workout.EditWorkout;

public class EditWorkoutCommandHandler : IRequestHandler<EditWorkoutCommand, Unit>
{
    private readonly IWorkoutRepository _repository;

    public EditWorkoutCommandHandler(IWorkoutRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Unit> Handle(EditWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workout = await _repository.GetWorkoutByIdAsync(request.WorkoutPlanId, cancellationToken);

        if (workout is null)
        {
            throw new WorkoutNotFound(request.WorkoutPlanId);
        }
        
        workout.ChangeName(new Name(request.Name));
        workout.ChangeDescription(new Description(request.Description));

        return Unit.Value;
    }
}