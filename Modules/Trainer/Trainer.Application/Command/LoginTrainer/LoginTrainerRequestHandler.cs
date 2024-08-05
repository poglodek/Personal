using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Trainer.Application.Exception;
using Trainer.Application.Repositories;

namespace Trainer.Application.Command.LoginTrainer;

public class LoginTrainerRequestHandler(ITrainerRepository repository, ILogger<LoginTrainerRequestHandler> logger, IPasswordHasher<Domain.Entity.Trainer> passwordHasher,) : IRequestHandler<LoginTrainerRequest, Unit>
{
    public async Task<Unit> Handle(LoginTrainerRequest request, CancellationToken cancellationToken)
    {
        var trainer = await repository.GetByEmail(request.Email, cancellationToken);

        await Task.Delay(Random.Shared.Next(300, 500), cancellationToken: cancellationToken);
        
        if (trainer is null)
        {
            logger.LogError("Trainer with email {Email} not found", request.Email);
            throw new TrainerNotFoundException(request.Email);
        }
        
        
        var result = passwordHasher.VerifyHashedPassword(trainer, trainer.Password.Hash, request.Password + trainer.Password.Salt);

        if (result == PasswordVerificationResult.Failed)
        {
            logger.LogError("Trainer with email {Email} has bad password", request.Email);
            throw new TrainerNotFoundException(request.Email);
        }
        
    }
}