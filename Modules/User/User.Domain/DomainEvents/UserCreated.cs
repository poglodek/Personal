using Shared.Core;

namespace User.Domain.DomainEvents;

public record UserCreated(Guid Id) : IDomainEvent;