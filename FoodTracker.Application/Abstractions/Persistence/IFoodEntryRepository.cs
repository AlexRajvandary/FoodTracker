using FoodTracker.Domain.Nutrition;

namespace FoodTracker.Application.Abstractions.Persistence;

public interface IFoodEntryRepository
{
    Task<IReadOnlyList<FoodEntry>> ListForUserAsync(
        Guid userId,
        DateTime? fromUtc,
        DateTime? toUtc,
        DateOnly? date,
        CancellationToken cancellationToken);

    Task<FoodEntry?> GetOwnedAsync(Guid userId, Guid entryId, CancellationToken cancellationToken);
    Task<FoodEntry> AddAsync(FoodEntry entry, CancellationToken cancellationToken);
    Task UpdateAsync(FoodEntry entry, CancellationToken cancellationToken);
    Task DeleteAsync(FoodEntry entry, CancellationToken cancellationToken);
}