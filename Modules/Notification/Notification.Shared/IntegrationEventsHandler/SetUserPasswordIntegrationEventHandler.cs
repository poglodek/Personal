using MediatR;
using Notification.Shared.IntegrationEvents;

namespace Notification.Shared.IntegrationEventsHandler;

public class SetUserPasswordIntegrationEventHandler : INotificationHandler<SetUserPasswordIntegrationEvent>
{
    public Task Handle(SetUserPasswordIntegrationEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}