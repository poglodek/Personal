using MediatR;

namespace Workout.Application.Command.WorkoutSession.StartWorkoutSession;

public record StartWorkoutSessionCommand(Guid SessionId) : IRequest;
