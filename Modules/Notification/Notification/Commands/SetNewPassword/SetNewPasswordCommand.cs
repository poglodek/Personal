using MediatR;

namespace Notification.Commands.SetNewPassword;

public record SetNewPasswordCommand(string Url, string Mail) : IRequest<Unit>;