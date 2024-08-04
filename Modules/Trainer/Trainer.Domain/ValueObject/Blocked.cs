namespace Trainer.Domain.ValueObject;

public record Blocked(DateTimeOffset StartedAt, string Reason);