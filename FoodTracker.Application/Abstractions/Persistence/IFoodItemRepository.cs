using FoodTracker.Domain.Nutrition;

namespace FoodTracker.Application.Abstractions.Persistence;

public interface IFoodItemRepository
{
    Task<FoodItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<FoodItem>> SearchByNameAsync(string query, CancellationToken cancellationToken);
    Task<IReadOnlyList<FoodItem>> ListCatalogAsync(string? query, string? category, CancellationToken cancellationToken);
    Task<FoodItem> AddAsync(FoodItem item, CancellationToken cancellationToken);
}
