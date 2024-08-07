using Auth;
using Funfair.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using User.Application.Exception;
using User.Application.Repositories;

namespace User.Application.Command.LoginUser;

public class LoginUserRequestHandler : IRequestHandler<LoginUserRequestCommand, JwtTokenDto>
{
    private readonly IUserRepository _repository;
    private readonly ILogger<LoginUserRequestHandler> _logger;
    private readonly IPasswordHasher<Domain.Entity.User> _passwordHasher;
    private readonly IJsonWebTokenManager _jsonWebTokenManager;
    private readonly TimeProvider _timeProvider;

    public LoginUserRequestHandler(IUserRepository repository, ILogger<LoginUserRequestHandler> logger, 
        IPasswordHasher<Domain.Entity.User> passwordHasher,IJsonWebTokenManager jsonWebTokenManager, TimeProvider timeProvider)
    {
        _repository = repository;
        _logger = logger;
        _passwordHasher = passwordHasher;
        _jsonWebTokenManager = jsonWebTokenManager;
        _timeProvider = timeProvider;
    }

    public async Task<JwtTokenDto> Handle(LoginUserRequestCommand requestCommand, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByEmail(requestCommand.Email, cancellationToken);
        
        await Task.Delay(Random.Shared.Next(300, 500), cancellationToken: cancellationToken);
        
        if (user is null)
        {
            _logger.LogError("User with email {Email} not found", requestCommand.Email);
            throw new UserNotFoundException(requestCommand.Email);
        }
        
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password.Hash, requestCommand.Password + user.Password.Salt);

        if (result == PasswordVerificationResult.Failed)
        {
            _logger.LogError("User with email {Email} typed bad password", requestCommand.Email);
            throw new UserNotFoundException(requestCommand.Email);
        }
        
        if (user.Activated is null)
        {
            _logger.LogError("User with id {id} has no Activated account", user.Id);
            throw new UserNotActivated(user.Id);
        }
        
        if(user.Blocked is not null)
        {
            _logger.LogError("User with id {id} has blocked account reason: {reason}", user.Id, user.Blocked.Reason);
            throw new UserBlockedException(user.Id);
        }
        
        var jwt = _jsonWebTokenManager.CreateToken(user.Id, user.MailAddress.Value, user.Role.Value, user.Claims.Select(x=>x.Value).ToList());

        user.SetLastLogin(_timeProvider);
        
        return jwt;
    }
}