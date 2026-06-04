using FluentValidation;

namespace FoodTracker.Application.Features.Activities;

public sealed class CreateActivityEntryCommandValidator : AbstractValidator<CreateActivityEntryCommand>
{
    public CreateActivityEntryCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ActivityTypeId).NotEmpty();
        RuleFor(x => x.OccurredAt).NotEqual(default(DateTime));
        When(x => x.DurationMinutes.HasValue, () => RuleFor(x => x.DurationMinutes!.Value).GreaterThan(0));
        When(x => x.RepetitionsCount.HasValue, () => RuleFor(x => x.RepetitionsCount!.Value).GreaterThan(0));
        When(x => x.CaloriesBurned.HasValue, () => RuleFor(x => x.CaloriesBurned!.Value).GreaterThanOrEqualTo(0));
    }
}
