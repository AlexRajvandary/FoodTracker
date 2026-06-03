using FluentValidation;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class SearchFoodItemsQueryValidator : AbstractValidator<SearchFoodItemsQuery>
{
    public SearchFoodItemsQueryValidator()
    {
        RuleFor(x => x.Query).NotEmpty().MinimumLength(1);
    }
}
