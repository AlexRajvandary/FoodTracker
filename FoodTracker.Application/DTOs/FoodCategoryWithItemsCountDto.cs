namespace FoodTracker.Application.DTOs;

public sealed class FoodCategoryWithItemsCountDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int FoodItemsCount { get; init; }
}