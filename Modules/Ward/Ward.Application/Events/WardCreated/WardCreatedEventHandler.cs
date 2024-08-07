using MediatR;

namespace Ward.Application.Events.WardCreated;

public class WardCreatedEventHandler : INotificationHandler<WardCreatedEvent>
{
    public Task Handle(WardCreatedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}