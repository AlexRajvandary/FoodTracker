using FluentValidation;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class SearchFoodItemsQueryValidator : AbstractValidator<SearchFoodItemsQuery>
{
    public SearchFoodItemsQueryValidator()
    {
        RuleFor(x => x.Q).NotEmpty().MinimumLength(1);
    }
}
