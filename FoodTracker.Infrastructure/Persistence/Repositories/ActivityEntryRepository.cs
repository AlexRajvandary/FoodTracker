using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Activities;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Infrastructure.Persistence.Repositories;

public class ActivityEntryRepository : IActivityEntryRepository
{
    private readonly DataContext _db;

    public ActivityEntryRepository(DataContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<ActivityEntry>> ListForUserAsync(
        Guid userId,
        DateTime? fromUtc,
        DateTime? toUtc,
        DateOnly? date,
        CancellationToken cancellationToken)
    {
        var q = _db.ActivityEntries.AsNoTracking().Where(x => x.UserId == userId);
        if (date is { } d)
        {
            var start = DateTime.SpecifyKind(d.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
            var end = DateTime.SpecifyKind(d.ToDateTime(TimeOnly.MaxValue), DateTimeKind.Utc);
            q = q.Where(x => x.OccurredAtUtc >= start && x.OccurredAtUtc <= end);
        }
        else
        {
            if (fromUtc is { } f)
            {
                q = q.Where(x => x.OccurredAtUtc >= f);
            }

            if (toUtc is { } t)
            {
                q = q.Where(x => x.OccurredAtUtc <= t);
            }

            if (fromUtc is null && toUtc is null && date is null)
            {
                var since = DateTime.UtcNow.AddDays(-30);
                q = q.Where(x => x.OccurredAtUtc >= since);
            }
        }

        return await q.OrderByDescending(x => x.OccurredAtUtc).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<ActivityEntry?> GetOwnedAsync(Guid userId, Guid entryId, CancellationToken cancellationToken)
    {
        return await _db
            .ActivityEntries.FirstOrDefaultAsync(x => x.Id == entryId && x.UserId == userId, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<ActivityEntry> AddAsync(ActivityEntry entry, CancellationToken cancellationToken)
    {
        _db.ActivityEntries.Add(entry);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return entry;
    }

    public async Task UpdateAsync(ActivityEntry entry, CancellationToken cancellationToken)
    {
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(ActivityEntry entry, CancellationToken cancellationToken)
    {
        _db.ActivityEntries.Remove(entry);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
