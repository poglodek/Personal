using MediatR;
using User.Application.Repositories;
using User.Domain.DomainEvents;

namespace User.Application.Events;

public class UserActivatedHandler : INotificationHandler<UserActivated>
{
    private readonly IUserRepository _repository;

    public UserActivatedHandler(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task Handle(UserActivated notification, CancellationToken cancellationToken)
    {
        var user = await _repository.GetById(notification.Id, cancellationToken);

        if (user?.Password is not null)
        {
            return;
        }
        
        //TODO: Send email to user with link to create password
        
    }
}