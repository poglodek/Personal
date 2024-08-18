using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Notification.Shared.IntegrationEvents;
using User.Application.Repositories;
using User.Domain.DomainEvents;

namespace User.Application.Events;

public class UserCreatedHandler : INotificationHandler<UserCreated>
{
    private readonly IUserRepository _repository;
    private readonly ILogger<UserCreatedHandler> _logger;
    private readonly IConfiguration _configuration;
    private readonly IPublisher _publisher;

    public UserCreatedHandler(IUserRepository repository, ILogger<UserCreatedHandler> logger, IConfiguration configuration, IPublisher publisher)
    {
        _repository = repository;
        _logger = logger;
        _configuration = configuration;
        _publisher = publisher;
    }

    public async Task Handle(UserCreated notification, CancellationToken cancellationToken)
    {
        var user = await _repository.GetById(notification.Id, cancellationToken);

        if (user is null)
        {
            _logger.LogError("User with id {Id} not found", notification.Id);
            return;
        }
        var link = $"{_configuration["LinkToApp"]}/user/activate/{notification.Id}/";
        
        await _publisher.Publish(new ActivateUserIntegrationEvent(link, user.MailAddress.Value), cancellationToken);
    }
}