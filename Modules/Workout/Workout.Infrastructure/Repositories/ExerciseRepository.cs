using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Workout.Application.Repositories;
using Workout.Domain.Entity;
using Workout.Infrastructure.Database;

namespace Workout.Infrastructure.Repositories;

internal class ExerciseRepository : IExerciseRepository
{
    private readonly WorkoutDbContext _dbContext;
    public ExerciseRepository(WorkoutDbContext dbContext, IMemoryCache cache)
    {
        _dbContext = dbContext;
    }
    
    public Task<Exercise?> GetExerciseById(Guid exerciseId, CancellationToken cancellationToken)
    {
        return _dbContext
            .Exercises
            .FirstOrDefaultAsync(x=> x.Id == exerciseId, cancellationToken);
    }

    public Task<Exercise?> GetExerciseBySetId(Guid setId, CancellationToken cancellationToken)
    {
        return _dbContext
            .Exercises
            .FirstOrDefaultAsync(x=> 
                    x.Sets.Any(c=>c.Id == setId)
                , cancellationToken);
    }

    public async Task AddExerciseAsync(Exercise exercise, CancellationToken cancellationToken)
    {
        await _dbContext.Exercises.AddAsync(exercise, cancellationToken);
    }
}