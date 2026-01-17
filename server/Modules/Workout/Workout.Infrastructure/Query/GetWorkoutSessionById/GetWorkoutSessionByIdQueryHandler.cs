using MediatR;
using Workout.Application.Dto;
using Workout.Application.Repositories;

namespace Workout.Infrastructure.Query.GetWorkoutSessionById;

public class GetWorkoutSessionByIdQueryHandler : IRequestHandler<GetWorkoutSessionByIdQuery, WorkoutSessionDto?>
{
    private readonly IWorkoutSessionRepository _repository;

    public GetWorkoutSessionByIdQueryHandler(IWorkoutSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<WorkoutSessionDto?> Handle(GetWorkoutSessionByIdQuery request, CancellationToken cancellationToken)
    {
        var session = await _repository.GetByIdAsync(request.SessionId, cancellationToken);
        return session?.MapToDto();
    }
}
