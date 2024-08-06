using Auth;
using Funfair.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using User.Application.Exception;
using User.Application.Repositories;

namespace User.Application.Command.LoginUser;

public class LoginUserRequestHandler(IUserRepository repository, ILogger<LoginUserRequestHandler> logger, 
    IPasswordHasher<Domain.Entity.User> passwordHasher,IJsonWebTokenManager jsonWebTokenManager) : IRequestHandler<LoginUserRequest, JwtTokenDto>
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
            logger.LogError("User with email {Email} has bad password", request.Email);
            throw new UserNotFoundException(request.Email);
        }
        
        return jsonWebTokenManager.CreateToken(user.Id, user.MailAddress.Value, user.Role.Value, user.Claims.Select(x=>x.Value).ToList());
    }
}