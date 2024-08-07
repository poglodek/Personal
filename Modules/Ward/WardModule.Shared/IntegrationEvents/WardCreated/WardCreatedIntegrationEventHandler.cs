using MediatR;
using Ward.Application.Events.WardAssignToTrainer;

namespace WardModule.Shared.IntegrationEvents.WardCreated;

public class WardCreatedIntegrationEventHandler : INotificationHandler<WardAssignToTrainerIntegrationEvent>
{
    private readonly IPublisher _publisher;

    public WardCreatedIntegrationEventHandler(IPublisher publisher)
    {
        _publisher = publisher;
    }
    
    public Task Handle(WardAssignToTrainerIntegrationEvent notification, CancellationToken cancellationToken)
    {
        return _publisher.Publish(new WardAssignToTrainerCommand(notification.TrainerId, notification.WardId), cancellationToken);
    }
}