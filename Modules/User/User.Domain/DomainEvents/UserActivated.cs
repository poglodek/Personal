using Shared.Core;

namespace User.Domain.DomainEvents;

public record UserActivated(Guid Id) : IDomainEvent;