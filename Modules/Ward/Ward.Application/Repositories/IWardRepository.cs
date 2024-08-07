namespace Ward.Application.Repositories;

public interface IWardRepository
{
    Task<Domain.Entity.Ward?> GetWardById(Guid id, CancellationToken cancellationToken);
    Task AddAsync(Domain.Entity.Ward ward, CancellationToken cancellationToken);
}