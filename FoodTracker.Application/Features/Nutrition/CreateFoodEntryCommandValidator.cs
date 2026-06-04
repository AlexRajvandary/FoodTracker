using FluentValidation;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class CreateFoodEntryCommandValidator : AbstractValidator<CreateFoodEntryCommand>
{
    public CreateFoodEntryCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.FoodItemId).NotEmpty();
        RuleFor(x => x.ConsumedAt).NotEqual(default(DateTime));
        RuleFor(x => x)
            .Must(x => (x.GramsConsumed is { } g && g > 0) || (x.PortionCount is { } p && p > 0))
            .WithMessage("Укажите gramsConsumed > 0 или portionCount > 0.");
    }
}
