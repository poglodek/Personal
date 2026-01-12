namespace User.Shared.Request;

public record SetPasswordRequest(Guid Id, string Password);