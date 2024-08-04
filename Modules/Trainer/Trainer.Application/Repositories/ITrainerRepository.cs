namespace Trainer.Application.Repositories;

public interface ITrainerRepository
{
    Task AddAsync(Domain.Entity.Trainer trainer, CancellationToken ct = default);
}