using Ward.Domain.ValueObject;

namespace Ward.Application.Repositories;

public interface IWardRepository
{
    Task<Domain.Entity.Ward?> GetWardById(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Domain.Entity.Ward ward, CancellationToken cancellationToken);
    Task<List<Domain.Entity.Ward>> GetTrainerWardsAsync(Guid requestId, CancellationToken cancellationToken);
    Task<Trainer?> GetWardTrainer(Guid requestWardId, CancellationToken cancellationToken);
}