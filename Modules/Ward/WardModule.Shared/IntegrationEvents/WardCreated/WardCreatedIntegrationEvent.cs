using Messaging.Integration.Events;

namespace WardModule.Shared.IntegrationEvents.WardCreated;

public record WardCreatedIntegrationEvent(Guid TrainerId, Guid WardId) : IIntegrationEvent;