using Microsoft.EntityFrameworkCore;
using Workout.Application.Repositories;
using Workout.Domain.Entity;
using Workout.Infrastructure.Database;

namespace Workout.Infrastructure.Repositories;

internal class WorkoutSessionRepository : IWorkoutSessionRepository
{
    private readonly WorkoutDbContext _dbContext;

    public WorkoutSessionRepository(WorkoutDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<WorkoutSession?> GetByIdAsync(Guid sessionId, CancellationToken ct = default)
    {
        return _dbContext.WorkoutSessions
            .TagWith("GetWorkoutSessionById")
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == sessionId, ct);
    }

    public async Task<IEnumerable<WorkoutSession>> GetByWardIdAsync(Guid wardId, DateOnly? startDate, DateOnly? endDate, CancellationToken ct = default)
    {
        var query = _dbContext.WorkoutSessions
            .TagWith("GetWorkoutSessionsByWardId")
            .Where(x => x.WardId == wardId)
            .AsSplitQuery();

        if (startDate.HasValue)
        {
            query = query.Where(x => x.ScheduledDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(x => x.ScheduledDate <= endDate.Value);
        }

        return await query
            .OrderBy(x => x.ScheduledDate)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<WorkoutSession>> GetByTrainerIdAsync(Guid trainerId, DateOnly? startDate, DateOnly? endDate, CancellationToken ct = default)
    {
        var query = _dbContext.WorkoutSessions
            .TagWith("GetWorkoutSessionsByTrainerId")
            .Where(x => x.TrainerId == trainerId)
            .AsSplitQuery();

        if (startDate.HasValue)
        {
            query = query.Where(x => x.ScheduledDate >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(x => x.ScheduledDate <= endDate.Value);
        }

        return await query
            .OrderBy(x => x.ScheduledDate)
            .ToListAsync(ct);
    }

    public async Task<IEnumerable<WorkoutSession>> GetByWorkoutIdAsync(Guid workoutId, CancellationToken ct = default)
    {
        return await _dbContext.WorkoutSessions
            .TagWith("GetWorkoutSessionsByWorkoutId")
            .Where(x => x.WorkoutId == workoutId)
            .AsSplitQuery()
            .OrderBy(x => x.ScheduledDate)
            .ToListAsync(ct);
    }

    public async Task AddAsync(WorkoutSession session, CancellationToken ct = default)
    {
        await _dbContext.WorkoutSessions.AddAsync(session, ct);
    }

    public async Task<bool> ExistsForWorkoutAndDateAsync(Guid workoutId, DateOnly scheduledDate, CancellationToken ct = default)
    {
        return await _dbContext.WorkoutSessions
            .AnyAsync(x => x.WorkoutId == workoutId && x.ScheduledDate == scheduledDate, ct);
    }
}
