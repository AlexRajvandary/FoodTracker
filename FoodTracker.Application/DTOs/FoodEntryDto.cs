namespace FoodTracker.Application.DTOs;

public class FoodEntryDto
{
    public double Calories { get; init; }
    public double Carbs { get; init; }
    public DateTime ConsumedAtUtc { get; init; }
    public double Fats { get; init; }
    public double? Fiber { get; init; }
    public Guid FoodId { get; init; }
    public double GramsConsumed { get; init; }
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public double Proteins { get; init; }
    public double? Salt { get; init; }
    public double? SaturatedFat { get; init; }
    public double? Sugars { get; init; }
}