using Shared.Core;

namespace Trainer.Domain.DomainEvents;

public record TrainerCreated(Guid Id) : IDomainEvent;