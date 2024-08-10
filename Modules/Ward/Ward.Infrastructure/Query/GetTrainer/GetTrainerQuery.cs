using MediatR;
using Ward.Infrastructure.Dto;

namespace Ward.Infrastructure.Query.GetTrainer;

//Return a Trainer who is a trainer of a ward
public record GetTrainerQuery(Guid WardId) : IRequest<TrainerDto>;