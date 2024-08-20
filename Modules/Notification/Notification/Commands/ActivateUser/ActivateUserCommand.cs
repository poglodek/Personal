using MediatR;

namespace Notification.Commands.ActivateUser;

public record ActivateUserCommand(string Url, string Mail) : IRequest<Unit>;