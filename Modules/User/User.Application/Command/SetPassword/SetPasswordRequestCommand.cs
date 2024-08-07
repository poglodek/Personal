using MediatR;

namespace User.Application.Command.SetPassword;

public record SetPasswordRequestCommand(Guid Id, string Password) : IRequest<Unit>;