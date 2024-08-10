using MediatR;
using Ward.Infrastructure.Dto;

namespace Ward.Infrastructure.Query.GetTrainerWards;

//Return a list of wards that a trainer is a trainer of
public record GetTrainerWardsQuery(Guid Id) : IRequest<List<WardDto>>;