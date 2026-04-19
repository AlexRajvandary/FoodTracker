using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Nutrition;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Infrastructure.Persistence.Repositories;

public class FoodEntryRepository : IFoodEntryRepository
{
    private readonly DataContext _db;

    public FoodEntryRepository(DataContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<FoodEntry>> ListForUserAsync(
        Guid userId,
        DateTime? fromUtc,
        DateTime? toUtc,
        DateOnly? date,
        CancellationToken cancellationToken)
    {
        var q = _db.FoodEntries.AsNoTracking().Where(x => x.UserId == userId);
        if (date is { } d)
        {
            var start = DateTime.SpecifyKind(d.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
            var end = DateTime.SpecifyKind(d.ToDateTime(TimeOnly.MaxValue), DateTimeKind.Utc);
            q = q.Where(x => x.ConsumedAtUtc >= start && x.ConsumedAtUtc <= end);
        }
        else
        {
            if (fromUtc is { } f)
            {
                q = q.Where(x => x.ConsumedAtUtc >= f);
            }

            if (toUtc is { } t)
            {
                q = q.Where(x => x.ConsumedAtUtc <= t);
            }

            if (fromUtc is null && toUtc is null && date is null)
            {
                var since = DateTime.UtcNow.AddDays(-30);
                q = q.Where(x => x.ConsumedAtUtc >= since);
            }
        }

        return await q.OrderByDescending(x => x.ConsumedAtUtc).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<FoodEntry?> GetOwnedAsync(Guid userId, Guid entryId, CancellationToken cancellationToken)
    {
        return await _db
            .FoodEntries.FirstOrDefaultAsync(x => x.Id == entryId && x.UserId == userId, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<FoodEntry> AddAsync(FoodEntry entry, CancellationToken cancellationToken)
    {
        _db.FoodEntries.Add(entry);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return entry;
    }

    public async Task UpdateAsync(FoodEntry entry, CancellationToken cancellationToken)
    {
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(FoodEntry entry, CancellationToken cancellationToken)
    {
        _db.FoodEntries.Remove(entry);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
