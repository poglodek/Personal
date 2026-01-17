using MediatR;
using Microsoft.AspNetCore.Http;
using Workout.Application.Repositories;
using Workout.Application.Services;
using Workout.Domain.ValueObject;
using Framework.Auth;

namespace Workout.Application.Command.WorkoutSession.RecordSetPerformance;

public class RecordSetPerformanceCommandHandler : IRequestHandler<RecordSetPerformanceCommand>
{
    private readonly IWorkoutSessionRepository _workoutSessionRepository;
    private readonly IWorkoutSessionAuthorizationService _authorizationService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RecordSetPerformanceCommandHandler(
        IWorkoutSessionRepository workoutSessionRepository,
        IWorkoutSessionAuthorizationService authorizationService,
        IHttpContextAccessor httpContextAccessor)
    {
        _workoutSessionRepository = workoutSessionRepository;
        _authorizationService = authorizationService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Handle(RecordSetPerformanceCommand request, CancellationToken cancellationToken)
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

        // Find the set performance
        SetPerformance? setPerformance = null;
        foreach (var exercisePerformance in session.ExercisePerformances)
        {
            setPerformance = exercisePerformance.SetPerformances.FirstOrDefault(sp => sp.Id == request.SetPerformanceId);
            if (setPerformance != null)
            {
                break;
            }
        }

        if (setPerformance == null)
        {
            throw new InvalidOperationException($"Set performance with ID {request.SetPerformanceId} not found in session");
        }

        // Build value objects
        Repeat? actualReps = request.ActualReps != null ? new Repeat(request.ActualReps) : null;
        RestTime? actualRestTime = request.ActualRestTimeSeconds.HasValue ? new RestTime(request.ActualRestTimeSeconds.Value) : null;
        Weight? weight = request.WeightValue.HasValue && !string.IsNullOrEmpty(request.WeightUnit)
            ? new Weight(request.WeightValue.Value, request.WeightUnit)
            : null;
        RepetitionRate? actualRepetitionRate = null;
        if (!string.IsNullOrEmpty(request.ActualRepetitionRateA))
        {
            actualRepetitionRate = new RepetitionRate(
                request.ActualRepetitionRateA ?? string.Empty,
                request.ActualRepetitionRateB ?? string.Empty,
                request.ActualRepetitionRateC ?? string.Empty,
                request.ActualRepetitionRateD ?? string.Empty
            );
        }
        Description? notes = !string.IsNullOrEmpty(request.Notes) ? new Description(request.Notes) : null;

        // Record performance
        setPerformance.RecordPerformance(
            actualReps,
            actualRestTime,
            weight,
            actualRepetitionRate,
            request.Completed,
            notes
        );
    }
}
