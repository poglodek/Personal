using Messaging.Integration.Events;

namespace Notification.Shared.IntegrationEvents;

public record ActivateUserIntegrationEvent(string Url, string Mail) : IIntegrationEvent;