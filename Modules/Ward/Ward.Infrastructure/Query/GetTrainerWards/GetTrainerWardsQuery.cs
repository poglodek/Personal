using MediatR;
using Ward.Infrastructure.Dto;

namespace Ward.Infrastructure.Query.GetTrainerWards;

public record GetTrainerWardsQuery(Guid Id) : IRequest<List<WardDto>>;