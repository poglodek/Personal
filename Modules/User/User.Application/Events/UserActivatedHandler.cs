using MediatR;
using Microsoft.Extensions.Configuration;
using Notification.Shared.IntegrationEvents;
using User.Application.Repositories;
using User.Domain.DomainEvents;

namespace User.Application.Events;

public class UserActivatedHandler : INotificationHandler<UserActivated>
{
    private readonly IUserRepository _repository;
    private readonly IConfiguration _configuration;
    private readonly IPublisher _publisher;

    public UserActivatedHandler(IUserRepository repository, IConfiguration configuration, IPublisher publisher)
    {
        _repository = repository;
        _configuration = configuration;
        _publisher = publisher;
    }
    public async Task Handle(UserActivated notification, CancellationToken cancellationToken)
    {
        var user = await _repository.GetById(notification.Id, cancellationToken);

        if (user?.Password is not null)
        {
            return;
        }
        
        var link = $"{_configuration["LinkToApp"]}/user/set-password/{user.Id}/";
        
        var integrationEvent = new SetUserPasswordIntegrationEvent(link, user.MailAddress.Value);
        
        await _publisher.Publish(integrationEvent, cancellationToken);
        
    }
}