namespace FoodTracker.Domain.Nutrition
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<FoodItemCountry> FoodItemCountries { get; set; } = [];
    }
}