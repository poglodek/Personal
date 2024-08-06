namespace User.Domain.ValueObject;

public record DateReason(DateTimeOffset StartedAt, string Reason);