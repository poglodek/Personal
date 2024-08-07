using MediatR;
using Messaging.Integration.Events;

namespace Messaging.Integration.Processor;

internal class IntegrationProcessor : IIntegrationProcessor
{
    private readonly IPublisher _publisher;

    public IntegrationProcessor(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public Task Publish(IIntegrationEvent @event, CancellationToken cancellationToken = default)
    {
        return _publisher.Publish(@event, cancellationToken);
    }
}