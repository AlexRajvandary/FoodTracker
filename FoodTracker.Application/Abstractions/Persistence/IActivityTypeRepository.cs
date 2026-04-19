using FoodTracker.Domain.Activities;

namespace FoodTracker.Application.Abstractions.Persistence;

public interface IActivityTypeRepository
{
    Task<ActivityType?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ActivityType>> ListOrSearchAsync(string? query, CancellationToken cancellationToken);
}
