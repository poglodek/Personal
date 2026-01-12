using MediatR;

namespace User.Application.Command.ActivateUser;

public record ActivateUserRequestCommand(Guid Id) : IRequest<Unit>;