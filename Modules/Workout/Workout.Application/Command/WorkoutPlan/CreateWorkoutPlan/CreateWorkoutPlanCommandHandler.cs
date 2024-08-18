using MediatR;
using Workout.Application.Dto;
using Workout.Application.Repositories;
using Workout.Domain.ValueObject;

namespace Workout.Application.Command.WorkoutPlan.CreateWorkoutPlan;

public class CreateWorkoutPlanCommandHandler : IRequestHandler<CreateWorkoutPlanCommand, WorkoutPlanDto>
{
    private readonly IWorkoutPlanRepository _repository;

    public CreateWorkoutPlanCommandHandler(IWorkoutPlanRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<WorkoutPlanDto> Handle(CreateWorkoutPlanCommand request, CancellationToken cancellationToken)
    {
        var workoutPlan = new Domain.Entity.WorkoutPlan(request.WardId, request.TrainerId, new Name(request.Name), new Description( request.Description));
        
        await _repository.AddWorkoutPlanAsync(workoutPlan, cancellationToken);

        return workoutPlan.MapToDto();
    }
}