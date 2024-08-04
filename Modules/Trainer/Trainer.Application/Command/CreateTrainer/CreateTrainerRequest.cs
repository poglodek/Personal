using MediatR;
using Trainer.Application.Dto;

namespace Trainer.Application.Command.CreateTrainer;

public record CreateTrainerRequest() : IRequest<TrainerDtoId>;