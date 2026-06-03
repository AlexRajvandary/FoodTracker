using FoodTracker.Domain.Nutrition;

namespace FoodTracker.Application.Abstractions.Persistence;

public interface IFoodItemRepository
{
    Task<FoodItem> CreateAsync(FoodItem foodItem, CancellationToken cancellationToken);
    Task DeleteAsync(FoodItem foodItem, CancellationToken cancellationToken);
    Task<FoodItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<FoodItem>> ListCatalogAsync(string? query, string? category, CancellationToken cancellationToken);
    Task<IReadOnlyList<FoodItem>> SearchByNameAsync(string query, CancellationToken cancellationToken);
}