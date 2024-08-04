using MediatR;
using Trainer.Application.Dto;

namespace Trainer.Application.Command.CreateTrainer;

public class CreateTrainerRequestHandler : IRequestHandler<CreateTrainerRequest,TrainerDtoId>
{
    public Task<TrainerDtoId> Handle(CreateTrainerRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}