using MediatR;
using Microsoft.AspNetCore.Identity;
using User.Application.Service.SaltService;
using User.Domain.ValueObject;

namespace User.Application.Command.CreatePassword;

public class CreatePasswordRequestHandler : IRequestHandler<CreatePasswordRequestCommand,Unit>
{
    private readonly IPasswordHasher<Domain.Entity.User> _passwordHasher;
    private readonly ISaltService _saltService;
    private readonly TimeProvider _timeProvider;

    public CreatePasswordRequestHandler(IPasswordHasher<Domain.Entity.User> passwordHasher, ISaltService saltService, TimeProvider timeProvider)
    {
        _passwordHasher = passwordHasher;
        _saltService = saltService;
        _timeProvider = timeProvider;
    }

    public Task<Unit> Handle(CreatePasswordRequestCommand requestCommand, CancellationToken cancellationToken)
    {
        var salt = _saltService.GenerateSalt();
        var passwordHash = _passwordHasher.HashPassword(requestCommand.User, requestCommand.Password + salt);
        
        var password = new PasswordHash(passwordHash,salt);

        requestCommand.User.SetPassword(password, _timeProvider);

        return Task.FromResult(Unit.Value);
    }
}