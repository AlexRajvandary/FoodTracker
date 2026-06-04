using FoodTracker.Domain.Activities;

namespace FoodTracker.Application.Abstractions.Persistence;

public interface IActivityEntryRepository
{
    Task<IReadOnlyList<ActivityEntry>> ListForUserAsync(
        Guid userId,
        DateTime? fromUtc,
        DateTime? toUtc,
        DateOnly? date,
        CancellationToken cancellationToken);

    Task<ActivityEntry?> GetOwnedAsync(Guid userId, Guid entryId, CancellationToken cancellationToken);
    Task<ActivityEntry> AddAsync(ActivityEntry entry, CancellationToken cancellationToken);
    Task UpdateAsync(ActivityEntry entry, CancellationToken cancellationToken);
    Task DeleteAsync(ActivityEntry entry, CancellationToken cancellationToken);
}
