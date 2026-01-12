using Shared.Core;

namespace User.Domain.DomainEvents;

public record WardCreated(Guid TrainerId, Guid WardId) : IDomainEvent;
