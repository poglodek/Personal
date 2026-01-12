using Microsoft.EntityFrameworkCore;
using Ward.Application.Repositories;
using Ward.Domain.ValueObject;
using Ward.Infrastructure.Database;

namespace Ward.Infrastructure.Repositories;

public class WardRepository : IWardRepository
{
    private readonly WardDbContext _dbContext;

    public WardRepository(WardDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<Domain.Entity.Ward?> GetWardById(Guid id, CancellationToken cancellationToken)
    {
        return _dbContext.Wards.FirstOrDefaultAsync(x=> x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Domain.Entity.Ward ward, CancellationToken cancellationToken)
    {
        await _dbContext.Wards.AddAsync(ward, cancellationToken);
    }

    public Task<List<Domain.Entity.Ward>> GetTrainerWardsAsync(Guid requestId, CancellationToken cancellationToken)
    {
        return _dbContext.Wards.Where(x => x.Trainer.Id == requestId).ToListAsync(cancellationToken);
    }

    public Task<Trainer?> GetWardTrainer(Guid requestWardId, CancellationToken cancellationToken)
    {
        return _dbContext.Wards.Where(x => x.Id == requestWardId).Select(x => x.Trainer).FirstOrDefaultAsync(cancellationToken);
    }
}