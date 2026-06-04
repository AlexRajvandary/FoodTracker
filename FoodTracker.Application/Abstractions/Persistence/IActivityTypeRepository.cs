using FoodTracker.Domain.Activities;

namespace FoodTracker.Application.Abstractions.Persistence;

public interface IActivityTypeRepository
{
    Task<ActivityType> CreateAsync(ActivityType activityType, CancellationToken cancellationToken);
    Task DeleteAsync(ActivityType activityType, CancellationToken cancellationToken);
    Task<ActivityType?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ActivityType?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ActivityType>> ListCatalogAsync(string? query, string? category, int page, int pageSize, CancellationToken cancellationToken);
}