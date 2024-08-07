using MediatR;

namespace Ward.Application.Events.WardAssignToTrainer;

public record WardAssignToTrainerCommand(Guid TrainerId, Guid WardId) : INotification;