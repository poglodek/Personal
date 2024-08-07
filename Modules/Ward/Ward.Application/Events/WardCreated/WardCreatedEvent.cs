using MediatR;

namespace Ward.Application.Events.WardCreated;

public record WardCreatedEvent(Guid TrainerId, Guid WardId) : INotification;