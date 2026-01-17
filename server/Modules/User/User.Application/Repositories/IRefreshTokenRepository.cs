namespace User.Application.Repositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(Domain.Entity.RefreshToken refreshToken, CancellationToken ct = default);
    Task<Domain.Entity.RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken ct = default);
    Task<List<Domain.Entity.RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId, CancellationToken ct = default);
}
