using MediatR;

namespace Workout.Application.Command.WorkoutSession.UpdateWorkoutSessionNotes;

public record UpdateWorkoutSessionNotesCommand(Guid SessionId, string Notes) : IRequest;
