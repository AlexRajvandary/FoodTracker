using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Activities;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Infrastructure.Persistence.Repositories;

public class ActivityTypeRepository : IActivityTypeRepository
{
    private const int DefaultTake = 200;
    private readonly DataContext _db;

    public ActivityTypeRepository(DataContext db)
    {
        _db = db;
    }

    public async Task<ActivityType?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ActivityTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<ActivityType>> ListOrSearchAsync(string? query, CancellationToken cancellationToken)
    {
        var q = _db.ActivityTypes.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(query))
        {
            var pattern = "%" + EscapeLike(query.Trim()) + "%";
            q = q.Where(x => EF.Functions.ILike(x.Name, pattern));
        }

        return await q.OrderBy(x => x.Name).Take(DefaultTake).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    private static string EscapeLike(string value) =>
        value.Replace("\\", "\\\\", StringComparison.Ordinal).Replace("%", "\\%", StringComparison.Ordinal).Replace("_", "\\_", StringComparison.Ordinal);
}
