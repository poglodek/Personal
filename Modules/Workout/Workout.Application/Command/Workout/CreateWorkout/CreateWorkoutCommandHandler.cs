using MediatR;
using Workout.Application.Dto;
using Workout.Application.Exception;
using Workout.Application.Repositories;
using Workout.Domain.ValueObject;

namespace Workout.Application.Command.Workout.CreateWorkout;

public class CreateWorkoutCommandHandler : IRequestHandler<CreateWorkoutCommand, WorkoutDto>
{
    private readonly IWorkoutPlanRepository _workoutPlanRepository;

    public CreateWorkoutCommandHandler(IWorkoutPlanRepository workoutPlanRepository)
    {
        _workoutPlanRepository = workoutPlanRepository;
    }
    
    public async Task<WorkoutDto> Handle(CreateWorkoutCommand request, CancellationToken cancellationToken)
    {
        var workoutPlan = await _workoutPlanRepository.GetWorkoutPlanByIdAsync(request.WorkoutPlanId, cancellationToken);
        if (workoutPlan == null)
        {
            throw new WorkoutPlanNotFound(request.WorkoutPlanId);
        }

        var workout = new Domain.Entity.Workout(new Name(request.Name), new Description(request.Description));
        
        workoutPlan.AddWorkout(workout);

        return workout.MapToDto();
    }
}