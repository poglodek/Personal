using MediatR;
using Notification.Commands.SetNewPassword;
using Notification.Shared.IntegrationEvents;

namespace Notification.Shared.IntegrationEventsHandler;

public class SetUserPasswordIntegrationEventHandler : INotificationHandler<SetUserPasswordIntegrationEvent>
{
    private readonly IMediator _mediator;

    public SetUserPasswordIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public Task Handle(SetUserPasswordIntegrationEvent notification, CancellationToken cancellationToken)
    {
        return _mediator.Send(new SetNewPasswordCommand(notification.Url, notification.Mail), cancellationToken);
    }
}