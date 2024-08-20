using MediatR;
using Notification.Commands.ActivateUser;
using Notification.Shared.IntegrationEvents;

namespace Notification.Shared.IntegrationEventsHandler;

public class ActivateUserIntegrationEventHandler : INotificationHandler<ActivateUserIntegrationEvent>
{
    private readonly IMediator _mediator;

    public ActivateUserIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Handle(ActivateUserIntegrationEvent notification, CancellationToken cancellationToken)
    {
        return _mediator.Send(new ActivateUserCommand(notification.Url, notification.Mail), cancellationToken);
    }
}