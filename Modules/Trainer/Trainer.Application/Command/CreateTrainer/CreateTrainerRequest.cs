using MediatR;
using Trainer.Application.Dto;

namespace Trainer.Application.Command.CreateTrainer;

public record CreateTrainerRequest(string FirstName, string LastName, string PhoneNumber, string MailAddress, string City, 
    string Street, string PostalCode, string County, DateOnly DateOfBirth, string Password) : IRequest<TrainerDtoId>;