using MediatR;
using User.Domain.DomainEvents;
using WardModule.Shared.IntegrationEvents;

namespace User.Application.Events;

public class WardCreatedHandler : INotificationHandler<WardCreated>
{
    private readonly IPublisher _publisher;

    public WardCreatedHandler(IPublisher publisher)
    {
        _publisher = publisher;
    }
    
    public Task Handle(WardCreated notification, CancellationToken cancellationToken)
    {
        return _publisher.Publish(new WardCreatedIntegrationEvent(notification.TrainerId, notification.WardId), cancellationToken);
    }
}