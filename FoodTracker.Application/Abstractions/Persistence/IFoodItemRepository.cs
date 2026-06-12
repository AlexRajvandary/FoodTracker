using FoodTracker.Application.DTOs;
using FoodTracker.Domain.Nutrition;

namespace FoodTracker.Application.Abstractions.Persistence;

public interface IFoodItemRepository
{
    Task<FoodItem> CreateAsync(FoodItem foodItem, CancellationToken cancellationToken);
    Task CreateCategoryAsync(FoodCategory category, CancellationToken cancellationToken);
    Task DeleteAsync(FoodItem foodItem, CancellationToken cancellationToken);
    Task<FoodCategory?> GetCategoryByNameAsync(string name, CancellationToken cancellationToken);
    Task<FoodItem?> GetByBarcodeAsync(string barcode, CancellationToken cancellationToken);
    Task<FoodItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<FoodItem?> GetByIdAsNoTrackingAsync(Guid id, CancellationToken cancellationToken);
    Task<PagedQueryResult<FoodItem>> ListCatalogAsync(IReadOnlyList<Guid> categoryIds, string? query, string? sortBy, bool SortDescending, int page, int pageSize, CancellationToken cancellationToken);
    Task<IReadOnlyList<FoodCategoryWithItemsCountDto>> ListCategoriesWithItemsCountAsync(CancellationToken cancellationToken);
}