using MediatR;
using Microsoft.AspNetCore.Identity;
using User.Application.Service.SaltService;
using User.Domain.ValueObject;

namespace User.Application.Command.CreatePassword;

public class CreatePasswordRequestHandler(IPasswordHasher<Domain.Entity.User> passwordHasher, ISaltService saltService) : IRequestHandler<CreatePasswordRequestCommand,Unit>
{
    public Task<Unit> Handle(CreatePasswordRequestCommand requestCommand, CancellationToken cancellationToken)
    {
        var salt = saltService.GenerateSalt();
        var passwordHash = passwordHasher.HashPassword(requestCommand.User, requestCommand.Password + salt);
        
        var password = new PasswordHash(passwordHash,salt);

        requestCommand.User.SetPassword(password);

        return Task.FromResult(Unit.Value);
    }
}