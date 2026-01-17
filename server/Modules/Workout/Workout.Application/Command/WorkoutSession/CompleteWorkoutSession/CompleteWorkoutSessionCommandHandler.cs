using MediatR;
using Microsoft.AspNetCore.Http;
using Workout.Application.Repositories;
using Workout.Application.Services;
using Workout.Domain.ValueObject;
using Framework.Auth;

namespace Workout.Application.Command.WorkoutSession.CompleteWorkoutSession;

public class CompleteWorkoutSessionCommandHandler : IRequestHandler<CompleteWorkoutSessionCommand>
{
    private readonly IWorkoutSessionRepository _workoutSessionRepository;
    private readonly IWorkoutSessionAuthorizationService _authorizationService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CompleteWorkoutSessionCommandHandler(
        IWorkoutSessionRepository workoutSessionRepository,
        IWorkoutSessionAuthorizationService authorizationService,
        IHttpContextAccessor httpContextAccessor)
    {
        _workoutSessionRepository = workoutSessionRepository;
        _authorizationService = authorizationService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Handle(CompleteWorkoutSessionCommand request, CancellationToken cancellationToken)
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

        // Complete the session
        session.CompleteSession();

        // Update notes if provided
        if (!string.IsNullOrEmpty(request.Notes))
        {
            session.UpdateNotes(new SessionNotes(request.Notes));
        }
    }
}
