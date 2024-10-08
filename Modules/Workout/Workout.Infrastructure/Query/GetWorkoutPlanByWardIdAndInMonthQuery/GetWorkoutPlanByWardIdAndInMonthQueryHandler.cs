using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Workout.Application.Dto;
using Workout.Application.Repositories;
using Workout.Infrastructure.Exception;

namespace Workout.Infrastructure.Query.GetWorkoutPlanByWardIdAndInMonthQuery;

public class GetWorkoutPlanByWardIdAndInMonthQueryHandler : IRequestHandler<GetWorkoutPlanByWardIdAndInMonthQuery, WorkoutPlanDto>
{
    private readonly IWorkoutPlanRepository _workoutRepository;

    public GetWorkoutPlanByWardIdAndInMonthQueryHandler(IWorkoutPlanRepository workoutRepository)
    {
        _workoutRepository = workoutRepository;
    }
    
    public async Task<WorkoutPlanDto> Handle(GetWorkoutPlanByWardIdAndInMonthQuery request, CancellationToken cancellationToken)
    {
        var workoutPlan = await _workoutRepository.GetWorkoutPlanByWardIdAndMonth(request.WardId, request.Month, cancellationToken);
        if (workoutPlan is null)
        {
            throw new WorkOutPlanNotFound(request.WardId, request.Month);
        }

        return workoutPlan.MapToDto();
    }
}