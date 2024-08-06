using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.Repositories;
using User.Domain.DomainEvents;

namespace User.Application.Events;

public class UserCreatedHandler(IUserRepository repository, ILogger<UserCreatedHandler> logger) : INotificationHandler<UserCreated>
{
    
    public async Task Handle(UserCreated notification, CancellationToken cancellationToken)
    {
        var User = await repository.GetById(notification.Id, cancellationToken);

        if (User is null)
        {
            logger.LogError("User with id {Id} not found", notification.Id);
            return;
        }
        
        //TODO: Send email to the User with the welcome message and activation link
    }
}