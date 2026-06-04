using FluentValidation;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class CreateFoodItemCommandValidator : AbstractValidator<CreateFoodItemCommand>
{
    public CreateFoodItemCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(500);
        RuleFor(x => x.CaloriesPer100g).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ProteinsPer100g).GreaterThanOrEqualTo(0).When(x => x.ProteinsPer100g.HasValue);
        RuleFor(x => x.FatsPer100g).GreaterThanOrEqualTo(0).When(x => x.FatsPer100g.HasValue);
        RuleFor(x => x.CarbsPer100g).GreaterThanOrEqualTo(0).When(x => x.CarbsPer100g.HasValue);
    }
}
