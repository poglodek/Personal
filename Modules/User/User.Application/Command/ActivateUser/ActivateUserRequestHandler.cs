using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.Exception;
using User.Application.Repositories;

namespace User.Application.Command.ActivateUser;

public class ActivateUserRequestHandler(IUserRepository repository, ILogger<ActivateUserRequestHandler> logger, TimeProvider timeProvider) : IRequestHandler<ActivateUserRequest,Unit>
{
    public async Task<Unit> Handle(ActivateUserRequest request, CancellationToken cancellationToken)
    {
        var User = await repository.GetById(request.Id, cancellationToken);
        if (User is null)
        {
            logger.LogError("User with id {Id} not found", request.Id);
            throw new UserNotFoundException(request.Id);
        }
        
        User.Activate(timeProvider);
        logger.LogInformation("Training with id {Id} activated", request.Id);
        
        return Unit.Value;
    }
}