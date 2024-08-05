using MediatR;
using Microsoft.Extensions.Logging;
using Trainer.Application.Exception;
using Trainer.Application.Repositories;

namespace Trainer.Application.Command.ActivateTrainer;

public class ActivateTrainerRequestHandler(ITrainerRepository repository, ILogger<ActivateTrainerRequestHandler> logger, TimeProvider timeProvider) : IRequestHandler<ActivateTrainerRequest,Unit>
{
    public async Task<Unit> Handle(ActivateTrainerRequest request, CancellationToken cancellationToken)
    {
        var trainer = await repository.GetById(request.Id, cancellationToken);
        if (trainer is null)
        {
            logger.LogError("Trainer with id {Id} not found", request.Id);
            throw new TrainerNotFoundException(request.Id);
        }
        
        trainer.Activate(timeProvider);
        logger.LogInformation("Training with id {Id} activated", request.Id);
        
        return Unit.Value;
    }
}