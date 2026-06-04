namespace FoodTracker.Domain.Nutrition;

/// <summary>Запись приёма пищи пользователя.</summary>
public class FoodEntry
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid FoodItemId { get; set; }
    public string FoodName { get; set; } = string.Empty;
    public DateTime ConsumedAtUtc { get; set; }
    public decimal GramsConsumed { get; set; }
    public string? PortionNote { get; set; }
    public decimal Calories { get; set; }
    public decimal Proteins { get; set; }
    public decimal Fats { get; set; }
    public decimal Carbs { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}
