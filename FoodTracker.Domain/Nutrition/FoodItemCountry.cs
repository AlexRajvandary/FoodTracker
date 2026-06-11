namespace FoodTracker.Domain.Nutrition
{
    public class FoodItemCountry
    {
        public Guid FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
    }
}
