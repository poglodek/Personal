using MediatR;
using Ward.Application.Repositories;
using Ward.Infrastructure.Dto;

namespace Ward.Infrastructure.Query.GetTrainerWards;

public class GetTrainerWardsQueryHandler : IRequestHandler<GetTrainerWardsQuery, List<WardDto>>
{
    private readonly IWardRepository _repository;

    public GetTrainerWardsQueryHandler(IWardRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<WardDto>> Handle(GetTrainerWardsQuery request, CancellationToken cancellationToken)
    {
        var wards = await _repository.GetTrainerWardsAsync(request.Id, cancellationToken);

        return wards.Select(x => new WardDto(x.Id)).ToList();
    }
}