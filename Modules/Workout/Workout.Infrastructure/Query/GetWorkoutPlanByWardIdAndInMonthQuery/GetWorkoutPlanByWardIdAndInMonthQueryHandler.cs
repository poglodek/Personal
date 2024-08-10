using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Workout.Application.Repositories;
using Workout.Infrastructure.Dto;
using Workout.Infrastructure.Exception;

namespace Workout.Infrastructure.Query.GetWorkoutPlanByWardIdAndInMonthQuery;

public class GetWorkoutPlanByWardIdAndInMonthQueryHandler : IRequestHandler<GetWorkoutPlanByWardIdAndInMonthQuery, WorkoutPlanDto>
{
    private readonly IWorkoutPlanRepository _workoutPlanRepository;

    public GetWorkoutPlanByWardIdAndInMonthQueryHandler(IWorkoutPlanRepository workoutPlanRepository)
    {
        _workoutPlanRepository = workoutPlanRepository;
    }
    
    public async Task<WorkoutPlanDto> Handle(GetWorkoutPlanByWardIdAndInMonthQuery request, CancellationToken cancellationToken)
    {
        var workoutPlan = await _workoutPlanRepository.GetWorkoutPlanByWardIdAndMonth(request.WardId, request.Month, cancellationToken);
        if (workoutPlan is null)
        {
            throw new WorkOutPlanNotFound(request.WardId, request.Month);
        }

        return workoutPlan.MapToDto();
    }
}