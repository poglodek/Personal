using MediatR;
using Microsoft.AspNetCore.Identity;
using Trainer.Application.Service.SaltService;
using Trainer.Domain.ValueObject;

namespace Trainer.Application.Command.CreatePassword;

public class CreatePasswordRequestHandler(IPasswordHasher<Domain.Entity.Trainer> passwordHasher, ISaltService saltService) : IRequestHandler<CreatePasswordRequest,Unit>
{
    public Task<Unit> Handle(CreatePasswordRequest request, CancellationToken cancellationToken)
    {
        var salt = saltService.GenerateSalt();
        var passwordHash = passwordHasher.HashPassword(request.Trainer, request.Password + salt);
        
        var password = new PasswordHash(passwordHash,salt);

        request.Trainer.SetPassword(password);

        return Task.FromResult(Unit.Value);
    }
}