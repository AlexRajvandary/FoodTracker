namespace FoodTracker.Application.Features.Nutrition;

public sealed class FoodEntryDto
{
    public Guid Id { get; init; }
    public Guid FoodItemId { get; init; }
    public string FoodName { get; init; } = string.Empty;
    public DateTime ConsumedAt { get; init; }
    public decimal GramsConsumed { get; init; }
    public string? PortionNote { get; init; }
    public decimal Calories { get; init; }
    public decimal Proteins { get; init; }
    public decimal Fats { get; init; }
    public decimal Carbs { get; init; }
}