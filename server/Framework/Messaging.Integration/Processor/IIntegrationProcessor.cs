using Messaging.Integration.Events;

namespace Messaging.Integration.Processor;

public interface IIntegrationProcessor
{
    Task Publish(IIntegrationEvent @event, CancellationToken cancellationToken = default);
}