using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Domain.Nutrition;
using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Infrastructure.Persistence.Repositories;

public class FoodItemRepository : IFoodItemRepository
{
    private const int DefaultTake = 100;
    private readonly DataContext _dataContext;

    public FoodItemRepository(DataContext db)
    {
        _dataContext = db;
    }

    public async Task<FoodItem> CreateAsync(FoodItem item, CancellationToken cancellationToken)
    {
        await _dataContext.FoodItems.AddAsync(item, cancellationToken).ConfigureAwait(false);
        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return item;
    }

    public async Task DeleteAsync(FoodItem item, CancellationToken cancellationToken)
    {
        _dataContext.FoodItems.Remove(item);
        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<FoodItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dataContext
            .FoodItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<FoodItem>> ListCatalogAsync(
       string? query,
       string? category,
       CancellationToken cancellationToken)
    {
        var foodItems = _dataContext
            .FoodItems
            .AsNoTracking()
            .Where(x => x.Category != null);

        if (!string.IsNullOrWhiteSpace(category))
        {
            foodItems = foodItems.Where(x => x.Category == category);
        }

        if (!string.IsNullOrWhiteSpace(query))
        {
            var pattern = "%" + EscapeLike(query.Trim()) + "%";
            foodItems = foodItems.Where(x => EF.Functions.ILike(x.Name, pattern));
        }

        return await foodItems
            .OrderBy(x => x.Name)
            .Take(DefaultTake)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<IReadOnlyList<FoodItem>> SearchByNameAsync(string query, CancellationToken cancellationToken)
    {
        if (query.Length == 0)
        {
            return [];
        }

        query = query.Trim();
        var pattern = "%" + EscapeLike(query) + "%";
        return await _dataContext
            .FoodItems
            .AsNoTracking()
            .Where(x => EF.Functions.ILike(x.Name, pattern))
            .OrderBy(x => x.Name)
            .Take(DefaultTake)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    private static string EscapeLike(string value) => value.Replace("\\", "\\\\", StringComparison.Ordinal).Replace("%", "\\%", StringComparison.Ordinal).Replace("_", "\\_", StringComparison.Ordinal);
}