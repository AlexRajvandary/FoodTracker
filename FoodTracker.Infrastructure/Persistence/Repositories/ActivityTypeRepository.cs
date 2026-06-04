using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Activities;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Infrastructure.Persistence.Repositories;

public class ActivityTypeRepository : IActivityTypeRepository
{
    private const int DefaultTake = 100;
    private readonly DataContext _dataContext;

    public ActivityTypeRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ActivityType> CreateAsync(ActivityType activityType, CancellationToken cancellationToken)
    {
        await _dataContext.AddAsync(activityType, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);
        return activityType;
    }

    public async Task DeleteAsync(ActivityType activityType, CancellationToken cancellationToken)
    {
        _dataContext.Remove(activityType);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<ActivityType?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dataContext.ActivityTypes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);
    }

    public async Task<ActivityType?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dataContext.ActivityTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<ActivityType>> ListCatalogAsync(string? query, string? category, CancellationToken cancellationToken)
    {
        var q = _dataContext.ActivityTypes.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query))
        {
            var pattern = "%" + EscapeLike(query.Trim()) + "%";
            q = q.Where(x => EF.Functions.ILike(x.Name, pattern));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            q = q.Where(activity => activity.Category == category);
        }

        return await q
            .OrderBy(activity => activity.Name)
            .Take(DefaultTake)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    private static string EscapeLike(string value) =>
        value.Replace("\\", "\\\\", StringComparison.Ordinal).Replace("%", "\\%", StringComparison.Ordinal).Replace("_", "\\_", StringComparison.Ordinal);
}