using MediatR;
using Ward.Application.Events.WardCreated;

namespace WardModule.Shared.IntegrationEvents.WardCreated;

public class WardCreatedIntegrationEventHandler : INotificationHandler<WardCreatedIntegrationEvent>
{
    private readonly IPublisher _publisher;

    public WardCreatedIntegrationEventHandler(IPublisher publisher)
    {
        _publisher = publisher;
    }
    
    public Task Handle(WardCreatedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        return _publisher.Publish(new WardCreatedEvent(notification.TrainerId, notification.WardId), cancellationToken);
    }
}