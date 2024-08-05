namespace Trainer.Application.Repositories;

public interface ITrainerRepository
{
    Task AddAsync(Domain.Entity.Trainer trainer, CancellationToken ct = default);
    Task<Domain.Entity.Trainer?> GetById(Guid notificationId, CancellationToken cancellationToken);
    Task<Domain.Entity.Trainer?> GetByEmail(string email, CancellationToken cancellationToken);
}