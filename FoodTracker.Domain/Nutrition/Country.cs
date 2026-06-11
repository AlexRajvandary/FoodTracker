using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Domain.Nutrition
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<FoodItemCountry> FoodItemCountries { get; set; } = [];
    }

    public class FoodItemCountry
    {
        public Guid FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
    }
}
