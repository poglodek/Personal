using MediatR;
using Microsoft.Extensions.Logging;
using Trainer.Application.Repositories;
using Trainer.Domain.DomainEvents;

namespace Trainer.Application.Events;

public class TrainerCreatedHandler(ITrainerRepository repository, ILogger<TrainerCreatedHandler> logger) : INotificationHandler<TrainerCreated>
{
    
    public async Task Handle(TrainerCreated notification, CancellationToken cancellationToken)
    {
        var trainer = await repository.GetById(notification.Id, cancellationToken);

        if (trainer is null)
        {
            logger.LogError("Trainer with id {Id} not found", notification.Id);
            return;
        }
        
        //TODO: Send email to the trainer with the welcome message and activation link
    }
}