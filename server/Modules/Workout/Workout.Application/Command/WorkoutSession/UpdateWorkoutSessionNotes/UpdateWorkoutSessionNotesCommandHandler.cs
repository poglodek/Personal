using MediatR;
using Microsoft.AspNetCore.Http;
using Workout.Application.Repositories;
using Workout.Application.Services;
using Workout.Domain.ValueObject;
using Framework.Auth;

namespace Workout.Application.Command.WorkoutSession.UpdateWorkoutSessionNotes;

public class UpdateWorkoutSessionNotesCommandHandler : IRequestHandler<UpdateWorkoutSessionNotesCommand>
{
    private readonly IWorkoutSessionRepository _workoutSessionRepository;
    private readonly IWorkoutSessionAuthorizationService _authorizationService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateWorkoutSessionNotesCommandHandler(
        IWorkoutSessionRepository workoutSessionRepository,
        IWorkoutSessionAuthorizationService authorizationService,
        IHttpContextAccessor httpContextAccessor)
    {
        _workoutSessionRepository = workoutSessionRepository;
        _authorizationService = authorizationService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Handle(UpdateWorkoutSessionNotesCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.GetUserId();

        // Authorize
        if (!await _authorizationService.UserCanAccessSession(userId, request.SessionId, cancellationToken))
        {
            throw new UnauthorizedAccessException("User cannot access this session");
        }

        // Load session
        var session = await _workoutSessionRepository.GetByIdAsync(request.SessionId, cancellationToken);
        if (session == null)
        {
            throw new InvalidOperationException($"Session with ID {request.SessionId} not found");
        }

        // Update notes
        session.UpdateNotes(new SessionNotes(request.Notes));
    }
}
