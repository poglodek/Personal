using MediatR;
using Trainer.Application.Command.CreatePassword;
using Trainer.Application.Dto;
using Trainer.Application.Repositories;
using Trainer.Domain.ValueObject;

namespace Trainer.Application.Command.CreateTrainer;

public class CreateTrainerRequestHandler(ITrainerRepository trainerRepository, IMediator mediator, 
    TimeProvider timeProvider) : IRequestHandler<CreateTrainerRequest,TrainerDtoId>
{
    public async Task<TrainerDtoId> Handle(CreateTrainerRequest request, CancellationToken cancellationToken)
    {
        var address = new Address(request.City, request.Street, request.PostalCode, request.County);
        
        var trainer = new Domain.Entity.Trainer(request.FirstName, request.LastName, new PhoneNumber(request.PhoneNumber),
            new Mail(request.MailAddress), address, request.DateOfBirth,  timeProvider);

        await mediator.Send(new CreatePasswordRequest(request.Password, trainer), cancellationToken);

        await trainerRepository.AddAsync(trainer, cancellationToken);

        return new TrainerDtoId(trainer.Id);
    }

    
}