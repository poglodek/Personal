using Messaging.Integration.Events;

namespace WardModule.Shared.IntegrationEvents.WardCreated;

public record WardAssignToTrainerIntegrationEvent(Guid TrainerId, Guid WardId) : IIntegrationEvent;