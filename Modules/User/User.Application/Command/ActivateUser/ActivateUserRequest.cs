using MediatR;

namespace User.Application.Command.ActivateUser;

public record ActivateUserRequest(Guid Id) : IRequest<Unit>;