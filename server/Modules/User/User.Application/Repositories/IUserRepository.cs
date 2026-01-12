namespace User.Application.Repositories;

public interface IUserRepository
{
    Task AddAsync(Domain.Entity.User User, CancellationToken ct = default);
    Task<Domain.Entity.User?> GetById(Guid notificationId, CancellationToken cancellationToken);
    Task<Domain.Entity.User?> GetByEmail(string email, CancellationToken cancellationToken);
}