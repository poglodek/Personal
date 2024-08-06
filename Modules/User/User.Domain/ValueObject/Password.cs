namespace User.Domain.ValueObject;

public record PasswordHash(string Hash, string Salt);