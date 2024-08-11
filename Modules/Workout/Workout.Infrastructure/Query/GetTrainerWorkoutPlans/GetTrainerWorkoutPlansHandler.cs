using MediatR;
using Workout.Application.Dto;
using Workout.Application.Repositories;

namespace Workout.Infrastructure.Query.GetTrainerWorkoutPlans;

public class GetTrainerWorkoutPlansQueryHandler : IRequestHandler<GetTrainerWorkoutPlansQuery,IReadOnlyList<WorkoutPlanDto>>
{
    private readonly IWorkoutPlanRepository _repository;

    public GetTrainerWorkoutPlansQueryHandler(IWorkoutPlanRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IReadOnlyList<WorkoutPlanDto>> Handle(GetTrainerWorkoutPlansQuery request, CancellationToken cancellationToken)
    {
        var workoutPlans = await _repository.GetByTrainerIdAsync(request.TrainerId, cancellationToken);
        
        return workoutPlans.Select(x => x.MapToDto()).ToList();
    }
}