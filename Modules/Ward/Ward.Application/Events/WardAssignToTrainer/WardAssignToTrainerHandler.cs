using MediatR;
using Ward.Application.Repositories;

namespace Ward.Application.Events.WardAssignToTrainer;

public class WardAssignToTrainerHandler : INotificationHandler<WardAssignToTrainerCommand>
{
    private readonly IWardRepository _wardRepository;

    public WardAssignToTrainerHandler(IWardRepository wardRepository)
    {
        _wardRepository = wardRepository;
    }
    
    public async Task Handle(WardAssignToTrainerCommand notification, CancellationToken cancellationToken)
    {
        var ward = await _wardRepository.GetWardById(notification.WardId, cancellationToken);
        if (ward is null)
        {
            ward = new Domain.Entity.Ward(notification.WardId, notification.TrainerId);
            await _wardRepository.AddAsync(ward, cancellationToken);
        }
        
        ward.AssignToTrainer(notification.TrainerId);
    }
}