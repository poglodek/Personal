using MediatR;
using Workout.Application.Dto;
using Workout.Application.Repositories;

namespace Workout.Infrastructure.Query.GetWorkoutSessionsByWorkoutId;

public class GetWorkoutSessionsByWorkoutIdQueryHandler : IRequestHandler<GetWorkoutSessionsByWorkoutIdQuery, List<WorkoutSessionDto>>
{
    private readonly IWorkoutSessionRepository _repository;

    public GetWorkoutSessionsByWorkoutIdQueryHandler(IWorkoutSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<WorkoutSessionDto>> Handle(GetWorkoutSessionsByWorkoutIdQuery request, CancellationToken cancellationToken)
    {
        var sessions = await _repository.GetByWorkoutIdAsync(request.WorkoutId, cancellationToken);
        return sessions.Select(s => s.MapToDto()).ToList();
    }
}
