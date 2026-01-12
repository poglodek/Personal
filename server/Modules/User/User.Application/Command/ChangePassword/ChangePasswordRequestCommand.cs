using MediatR;

namespace User.Application.Command.ChangePassword;

public record ChangePasswordRequestCommand(Guid Id, string Password) : IRequest<Unit>;