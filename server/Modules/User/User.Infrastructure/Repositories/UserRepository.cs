using Microsoft.EntityFrameworkCore;
using User.Application.Repositories;
using User.Infrastructure.Database;

namespace User.Infrastructure.Repositories;

internal class UserRepository(UserDbContext userDbContext) : IUserRepository
{
    public async Task AddAsync(Domain.Entity.User user, CancellationToken ct = default)
    {
        await userDbContext.Users.AddAsync(user, ct);
    }

    public Task<Domain.Entity.User?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
       return userDbContext.Users.FirstOrDefaultAsync(x=> x.Id == id, cancellationToken);
    }

    public Task<Domain.Entity.User?> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        return userDbContext.Users.FirstOrDefaultAsync(x=> x.MailAddress.Value == email, cancellationToken);
    }
}