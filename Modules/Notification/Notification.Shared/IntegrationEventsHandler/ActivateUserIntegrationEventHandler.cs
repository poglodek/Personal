using MediatR;
using Notification.Shared.IntegrationEvents;

namespace Notification.Shared.IntegrationEventsHandler;

public class ActivateUserIntegrationEventHandler : INotificationHandler<ActivateUserIntegrationEvent>
{
    public Task Handle(ActivateUserIntegrationEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}