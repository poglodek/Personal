using MediatR;
using Ward.Infrastructure.Dto;

namespace Ward.Infrastructure.Query.GetTrainer;

public record GetTrainerQuery(Guid WardId) : IRequest<TrainerDto>;