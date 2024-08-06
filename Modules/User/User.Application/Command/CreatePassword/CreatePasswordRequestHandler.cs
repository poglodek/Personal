using MediatR;
using Microsoft.AspNetCore.Identity;
using User.Application.Service.SaltService;
using User.Domain.ValueObject;

namespace User.Application.Command.CreatePassword;

public class CreatePasswordRequestHandler(IPasswordHasher<Domain.Entity.User> passwordHasher, ISaltService saltService) : IRequestHandler<CreatePasswordRequest,Unit>
{
    public Task<Unit> Handle(CreatePasswordRequest request, CancellationToken cancellationToken)
    {
        var salt = saltService.GenerateSalt();
        var passwordHash = passwordHasher.HashPassword(request.User, request.Password + salt);
        
        var password = new PasswordHash(passwordHash,salt);

        request.User.SetPassword(password);

        return Task.FromResult(Unit.Value);
    }
}