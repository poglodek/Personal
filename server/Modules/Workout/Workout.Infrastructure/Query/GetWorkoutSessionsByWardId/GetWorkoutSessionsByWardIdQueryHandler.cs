using MediatR;
using Workout.Application.Dto;
using Workout.Application.Repositories;

namespace Workout.Infrastructure.Query.GetWorkoutSessionsByWardId;

public class GetWorkoutSessionsByWardIdQueryHandler : IRequestHandler<GetWorkoutSessionsByWardIdQuery, List<WorkoutSessionDto>>
{
    private readonly IWorkoutSessionRepository _repository;

    public GetWorkoutSessionsByWardIdQueryHandler(IWorkoutSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<WorkoutSessionDto>> Handle(GetWorkoutSessionsByWardIdQuery request, CancellationToken cancellationToken)
    {
        var sessions = await _repository.GetByWardIdAsync(request.WardId, request.StartDate, request.EndDate, cancellationToken);
        return sessions.Select(s => s.MapToDto()).ToList();
    }
}
