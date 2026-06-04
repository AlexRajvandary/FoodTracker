using FluentValidation;

namespace FoodTracker.Application.Features.Nutrition;

public sealed class DeleteFoodEntryCommandValidator : AbstractValidator<DeleteFoodEntryCommand>
{
    public DeleteFoodEntryCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.EntryId).NotEmpty();
    }
}
