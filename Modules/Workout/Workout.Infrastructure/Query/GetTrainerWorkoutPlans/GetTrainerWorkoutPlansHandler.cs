using MediatR;
using Workout.Application.Repositories;
using Workout.Infrastructure.Dto;

namespace Workout.Infrastructure.Query.GetTrainerWorkoutPlans;

public class GetTrainerWorkoutPlansQueryHandler : IRequestHandler<GetTrainerWorkoutPlansQuery,IReadOnlyList<WorkoutPlanDto>>
{
    private readonly IWorkoutRepository _repository;

    public GetTrainerWorkoutPlansQueryHandler(IWorkoutRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IReadOnlyList<WorkoutPlanDto>> Handle(GetTrainerWorkoutPlansQuery request, CancellationToken cancellationToken)
    {
        var workoutPlans = await _repository.GetByTrainerIdAsync(request.TrainerId, cancellationToken);
        
        return workoutPlans.Select(x => x.MapToDto()).ToList();
    }
}