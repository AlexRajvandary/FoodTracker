namespace FoodTracker.Application.DTOs;

public class FoodEntryDto
{
    public double Calories { get; init; }
    public double Carbs { get; init; }
    public DateTime ConsumedAtUtc { get; init; }
    public double Fats { get; init; }
    public Guid FoodId { get; init; }
    public double Grams { get; init; }
    public Guid Id { get; init;  }
    public required string Name { get; init;  }
    public double Proteins { get; init; }
}