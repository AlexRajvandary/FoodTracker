namespace FoodTracker.Domain.Nutrition;

public class FoodEntry
{
    public double Calories { get; set; }
    public double Carbs { get; set; }
    public DateTime ConsumedAtUtc { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public double Fats { get; set; }
    public double? Fiber { get; set; }
    public Guid FoodId { get; set; }
    public string FoodName { get; set; } = string.Empty;
    public double GramsConsumed { get; set; }
    public Guid Id { get; set; }
    public double Proteins { get; set; }
    public double? Salt { get; set; }
    public double? SaturatedFat { get; set; }
    public double? Sugars { get; set; }
    public Guid UserId { get; set; }
}