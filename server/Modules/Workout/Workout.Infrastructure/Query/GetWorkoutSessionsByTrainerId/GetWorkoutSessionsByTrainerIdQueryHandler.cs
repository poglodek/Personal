using MediatR;
using Workout.Application.Dto;
using Workout.Application.Repositories;

namespace Workout.Infrastructure.Query.GetWorkoutSessionsByTrainerId;

public class GetWorkoutSessionsByTrainerIdQueryHandler : IRequestHandler<GetWorkoutSessionsByTrainerIdQuery, List<WorkoutSessionDto>>
{
    private readonly IWorkoutSessionRepository _repository;

    public GetWorkoutSessionsByTrainerIdQueryHandler(IWorkoutSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<WorkoutSessionDto>> Handle(GetWorkoutSessionsByTrainerIdQuery request, CancellationToken cancellationToken)
    {
        var sessions = await _repository.GetByTrainerIdAsync(request.TrainerId, request.StartDate, request.EndDate, cancellationToken);
        return sessions.Select(s => s.MapToDto()).ToList();
    }
}
