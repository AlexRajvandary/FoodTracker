namespace FoodTracker.Domain.Nutrition;

public class FoodEntry
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid FoodId { get; set; }
    public string FoodName { get; set; } = string.Empty;
    public DateTime ConsumedAtUtc { get; set; }
    public double GramsConsumed { get; set; }
    public double Calories { get; set; }
    public double Proteins { get; set; }
    public double Fats { get; set; }
    public double Carbs { get; set; }
    public DateTime CreatedAtUtc { get; set; }
}