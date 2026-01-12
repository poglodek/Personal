using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.Exception;
using User.Application.Repositories;

namespace User.Application.Command.ActivateUser;

public class ActivateUserRequestHandler : IRequestHandler<ActivateUserRequestCommand,Unit>
{
    private readonly IUserRepository _repository;
    private readonly ILogger<ActivateUserRequestHandler> _logger;
    private readonly TimeProvider _timeProvider;

    public ActivateUserRequestHandler(IUserRepository repository, ILogger<ActivateUserRequestHandler> logger, TimeProvider timeProvider)
    {
        _repository = repository;
        _logger = logger;
        _timeProvider = timeProvider;
    }

    public async Task<Unit> Handle(ActivateUserRequestCommand requestCommand, CancellationToken cancellationToken)
    {
        var user = await _repository.GetById(requestCommand.Id, cancellationToken);
        if (user is null)
        {
            _logger.LogError("User with id {Id} not found", requestCommand.Id);
            throw new UserNotFoundException(requestCommand.Id);
        }
        
        user.Activate(_timeProvider);
        _logger.LogInformation("Training with id {Id} activated", requestCommand.Id);
        
        return Unit.Value;
    }
}