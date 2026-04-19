using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Nutrition;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Infrastructure.Persistence.Repositories;

public class FoodItemRepository : IFoodItemRepository
{
    private const int DefaultTake = 100;
    private readonly DataContext _db;

    public FoodItemRepository(DataContext db)
    {
        _db = db;
    }

    public async Task<FoodItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.FoodItems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken).ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<FoodItem>> SearchByNameAsync(string query, CancellationToken cancellationToken)
    {
        var q = query.Trim();
        if (q.Length == 0)
        {
            return Array.Empty<FoodItem>();
        }

        var pattern = "%" + EscapeLike(q) + "%";
        return await _db
            .FoodItems.AsNoTracking()
            .Where(x => EF.Functions.ILike(x.Name, pattern))
            .OrderBy(x => x.Name)
            .Take(DefaultTake)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<FoodItem>> ListCatalogAsync(
        string? query,
        string? category,
        CancellationToken cancellationToken)
    {
        var q = _db.FoodItems.AsNoTracking().Where(x => x.Category != null);
        if (!string.IsNullOrWhiteSpace(category))
        {
            q = q.Where(x => x.Category == category);
        }

        if (!string.IsNullOrWhiteSpace(query))
        {
            var pattern = "%" + EscapeLike(query.Trim()) + "%";
            q = q.Where(x => EF.Functions.ILike(x.Name, pattern));
        }

        return await q.OrderBy(x => x.Name).Take(DefaultTake).ToListAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<FoodItem> AddAsync(FoodItem item, CancellationToken cancellationToken)
    {
        _db.FoodItems.Add(item);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return item;
    }

    private static string EscapeLike(string value) => value.Replace("\\", "\\\\", StringComparison.Ordinal).Replace("%", "\\%", StringComparison.Ordinal).Replace("_", "\\_", StringComparison.Ordinal);
}
