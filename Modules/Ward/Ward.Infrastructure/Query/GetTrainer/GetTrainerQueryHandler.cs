using MediatR;
using Ward.Application.Repositories;
using Ward.Infrastructure.Dto;
using Ward.Infrastructure.Exception;

namespace Ward.Infrastructure.Query.GetTrainer;

public class GetTrainerQueryHandler : IRequestHandler<GetTrainerQuery,TrainerDto>
{
    private readonly IWardRepository _repository;

    public GetTrainerQueryHandler(IWardRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<TrainerDto> Handle(GetTrainerQuery request, CancellationToken cancellationToken)
    {
        var trainer = await _repository.GetWardTrainer(request.WardId, cancellationToken);

        if (trainer is null)
        {
            throw new TrainerNotFound(request.WardId);
        }

        return new TrainerDto(trainer.Id);
    }
}