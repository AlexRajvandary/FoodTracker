using FoodTracker.Application.Abstractions;
using FoodTracker.Application.Abstractions.Persistence;
using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Nutrition;

using Microsoft.EntityFrameworkCore;

namespace FoodTracker.Infrastructure.Persistence.Repositories;

public class FoodItemRepository : IFoodItemRepository
{
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

    public async Task<FoodItem?> GetByBarcodeAsync(string barcode, CancellationToken cancellationToken)
    {
        return await _dataContext.FoodItems
        .AsNoTracking()
        .Include(x => x.FoodItemCategories)
            .ThenInclude(x => x.FoodCategory)
        .Include(x => x.FoodItemCountries)
            .ThenInclude(x => x.Country)
        .FirstOrDefaultAsync(
            x => x.Barcode == barcode,
            cancellationToken)
        .ConfigureAwait(false);
    }

    public async Task<FoodCategory?> GetCategoryByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _dataContext.FoodCategories
            .FirstOrDefaultAsync(
                x => x.Name.ToLower() == name.ToLower(),
                cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task CreateCategoryAsync(FoodCategory category, CancellationToken cancellationToken)
    {
        await _dataContext.FoodCategories
            .AddAsync(category, cancellationToken)
            .ConfigureAwait(false);

        await _dataContext.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<FoodItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dataContext
            .FoodItems
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<FoodItem?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dataContext
            .FoodItems
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<PagedQueryResult<FoodItem>> ListCatalogAsync(IReadOnlyList<Guid> categoryIds,
                                                                   string? query,
                                                                   string? sortBy,
                                                                   bool sortDescending,
                                                                   int page,
                                                                   int pageSize,
                                                                   CancellationToken cancellationToken)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var foodItems = _dataContext
            .FoodItems
            .AsNoTracking()
            .AsQueryable();

        if (categoryIds.Count > 0)
        {
            var distinctCategoryIds = categoryIds
                .Distinct()
                .ToList();

            foodItems = foodItems.Where(x =>
                x.FoodItemCategories.Any(c =>
                    distinctCategoryIds.Contains(c.FoodCategoryId)));
        }

        if (!string.IsNullOrWhiteSpace(query))
        {
            var pattern = "%" + EscapeLike(query.Trim()) + "%";

            foodItems = foodItems.Where(foodItem =>
                EF.Functions.ILike(foodItem.Name, pattern) ||
                (foodItem.Brand != null && EF.Functions.ILike(foodItem.Brand, pattern)) ||
                EF.Functions.ILike(foodItem.Barcode, pattern));
        }

        foodItems = (sortBy ?? "name").ToLowerInvariant() switch
        {
            "brand" => sortDescending
                ? foodItems.OrderByDescending(x => x.Brand)
                : foodItems.OrderBy(x => x.Brand),

            "calories" => sortDescending
                ? foodItems.OrderByDescending(x => x.CaloriesPer100g)
                : foodItems.OrderBy(x => x.CaloriesPer100g),

            "carbs" => sortDescending
                ? foodItems.OrderByDescending(x => x.CarbsPer100g)
                : foodItems.OrderBy(x => x.CarbsPer100g),

            "createdat" => sortDescending
                ? foodItems.OrderByDescending(x => x.CreatedAtUtc)
                : foodItems.OrderBy(x => x.CreatedAtUtc),

            "fats" => sortDescending
                ? foodItems.OrderByDescending(x => x.FatsPer100g)
                : foodItems.OrderBy(x => x.FatsPer100g),

            "fiber" => sortDescending
                ? foodItems.OrderByDescending(x => x.FiberPer100g)
                : foodItems.OrderBy(x => x.FiberPer100g),

            "proteins" => sortDescending
                ? foodItems.OrderByDescending(x => x.ProteinsPer100g)
                : foodItems.OrderBy(x => x.ProteinsPer100g),

            "salt" => sortDescending
                ? foodItems.OrderByDescending(x => x.SaltPer100g)
                : foodItems.OrderBy(x => x.SaltPer100g),

            "sugars" => sortDescending
                ? foodItems.OrderByDescending(x => x.SugarsPer100g)
                : foodItems.OrderBy(x => x.SugarsPer100g),

            _ => sortDescending
                ? foodItems.OrderByDescending(x => x.Name)
                : foodItems.OrderBy(x => x.Name)
        };

        var totalCount = await foodItems
            .CountAsync(cancellationToken)
            .ConfigureAwait(false);

        var items = await foodItems
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Include(x => x.FoodItemCategories)
                .ThenInclude(x => x.FoodCategory)
            .Include(x => x.FoodItemCountries)
                .ThenInclude(x => x.Country)
            .AsSplitQuery()
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);

        return new PagedQueryResult<FoodItem>
        {
            Items = items,
            TotalCount = totalCount
        };
    }

    private static string EscapeLike(string value) => value.Replace("\\", "\\\\", StringComparison.Ordinal).Replace("%", "\\%", StringComparison.Ordinal).Replace("_", "\\_", StringComparison.Ordinal);

    public async Task<IReadOnlyList<FoodCategoryWithItemsCountDto>> ListCategoriesWithItemsCountAsync(CancellationToken cancellationToken)
    {
        return await _dataContext.FoodCategories
           .AsNoTracking()
           .Select(category => new FoodCategoryWithItemsCountDto
           {
               Id = category.Id,
               Name = category.Name,
               FoodItemsCount = category.FoodItemCategories
                   .Select(x => x.FoodItemId)
                   .Distinct()
                   .Count()
           })
           .OrderBy(x => x.Name)
           .ToListAsync(cancellationToken);
    }
}