using Messaging.Integration.Events;

namespace WardModule.Shared.IntegrationEvents;

public record WardCreatedIntegrationEvent(Guid TrainerId, Guid WardId) : IIntegrationEvent;