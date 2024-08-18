using Messaging.Integration.Events;

namespace Notification.Shared.IntegrationEvents;

public record SetUserPasswordIntegrationEvent(string Url, string Mail) : IIntegrationEvent;