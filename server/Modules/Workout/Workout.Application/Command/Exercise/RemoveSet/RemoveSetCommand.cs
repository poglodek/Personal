using MediatR;

namespace Workout.Application.Command.Exercise.RemoveSet;

public record RemoveSetCommand(Guid Id) : IRequest<Unit>;