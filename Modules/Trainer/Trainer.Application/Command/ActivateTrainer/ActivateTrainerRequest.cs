using MediatR;

namespace Trainer.Application.Command.ActivateTrainer;

public record ActivateTrainerRequest(Guid Id) : IRequest<Unit>;