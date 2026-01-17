using MediatR;

namespace Workout.Application.Command.WorkoutSession.CompleteWorkoutSession;

public record CompleteWorkoutSessionCommand(Guid SessionId, string? Notes) : IRequest;
