using FluentValidation;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class UpdateFoodEntryCommandValidator : AbstractValidator<UpdateFoodEntryCommand>
{
    public UpdateFoodEntryCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.EntryId).NotEmpty();
        RuleFor(x => x)
            .Must(x => x.GramsConsumed.HasValue || x.ConsumedAt.HasValue || x.PortionNote is not null)
            .WithMessage("Нет полей для обновления.");
        When(x => x.GramsConsumed.HasValue, () => RuleFor(x => x.GramsConsumed!).GreaterThan(0));
        When(x => x.ConsumedAt.HasValue, () => RuleFor(x => x.ConsumedAt!).NotEqual(default(DateTime)));
    }
}
