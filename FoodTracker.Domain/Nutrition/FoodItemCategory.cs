namespace FoodTracker.Domain.Nutrition;

public class FoodItemCategory
{
    public Guid FoodCategoryId { get; set; }
    public FoodCategory FoodCategory { get; set; } = null!;
    public FoodItem FoodItem { get; set; } = null!;
    public Guid FoodItemId { get; set; }
}