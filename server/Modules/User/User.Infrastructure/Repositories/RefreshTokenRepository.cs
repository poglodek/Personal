using Microsoft.EntityFrameworkCore;
using User.Application.Repositories;
using User.Infrastructure.Database;

namespace User.Infrastructure.Repositories;

internal class RefreshTokenRepository(UserDbContext userDbContext) : IRefreshTokenRepository
{
    public async Task AddAsync(Domain.Entity.RefreshToken refreshToken, CancellationToken ct = default)
    {
        await userDbContext.RefreshTokens.AddAsync(refreshToken, ct);
    }

    public Task<Domain.Entity.RefreshToken?> GetByTokenHashAsync(string tokenHash, CancellationToken ct = default)
    {
        return userDbContext.RefreshTokens
            .FirstOrDefaultAsync(x => x.TokenHash == tokenHash, ct);
    }

    public Task<List<Domain.Entity.RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        var now = DateTimeOffset.UtcNow;
        return userDbContext.RefreshTokens
            .Where(x => x.UserId == userId && x.RevokedAt == null && x.ExpiresAt > now)
            .ToListAsync(ct);
    }
}
