using Auth;
using Funfair.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using User.Application.Exception;
using User.Application.Repositories;

namespace User.Application.Command.LoginUser;

public class LoginUserRequestHandler(IUserRepository repository, ILogger<LoginUserRequestHandler> logger, 
    IPasswordHasher<Domain.Entity.User> passwordHasher,IJsonWebTokenManager jsonWebTokenManager, TimeProvider timeProvider) : IRequestHandler<LoginUserRequest, JwtTokenDto>
{
    public async Task<JwtTokenDto> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByEmail(request.Email, cancellationToken);
        
        await Task.Delay(Random.Shared.Next(300, 500), cancellationToken: cancellationToken);
        
        if (user is null)
        {
            logger.LogError("User with email {Email} not found", request.Email);
            throw new UserNotFoundException(request.Email);
        }
        
        var result = passwordHasher.VerifyHashedPassword(user, user.Password.Hash, request.Password + user.Password.Salt);

        if (result == PasswordVerificationResult.Failed)
        {
            logger.LogError("User with email {Email} typed bad password", request.Email);
            throw new UserNotFoundException(request.Email);
        }
        
        if (user.Activated is null)
        {
            logger.LogError("User with id {id} has no Activated account", user.Id.Value);
            throw new UserNotActivated(user.Id.Value);
        }
        
        if(user.Blocked is not null)
        {
            logger.LogError("User with id {id} has blocked account reason: {reason}", user.Id.Value, user.Blocked.Reason);
            throw new UserBlockedException(user.Id.Value);
        }
        
        var jwt = jsonWebTokenManager.CreateToken(user.Id, user.MailAddress.Value, user.Role.Value, user.Claims.Select(x=>x.Value).ToList());

        user.SetLastLogin(timeProvider);
        
        return jwt;
    }
}