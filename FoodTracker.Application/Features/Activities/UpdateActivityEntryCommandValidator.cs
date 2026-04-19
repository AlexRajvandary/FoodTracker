using FluentValidation;

namespace FoodTracker.Application.Features.Activities;

public sealed class UpdateActivityEntryCommandValidator : AbstractValidator<UpdateActivityEntryCommand>
{
    public UpdateActivityEntryCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.EntryId).NotEmpty();
        RuleFor(x => x)
            .Must(x => x.OccurredAt.HasValue || x.DurationMinutes.HasValue || x.RepetitionsCount.HasValue || x.CaloriesBurned.HasValue)
            .WithMessage("Нет полей для обновления.");
        When(x => x.OccurredAt.HasValue, () => RuleFor(x => x.OccurredAt!.Value).NotEqual(default(DateTime)));
        When(x => x.DurationMinutes.HasValue, () => RuleFor(x => x.DurationMinutes!.Value).GreaterThan(0));
        When(x => x.RepetitionsCount.HasValue, () => RuleFor(x => x.RepetitionsCount!.Value).GreaterThan(0));
        When(x => x.CaloriesBurned.HasValue, () => RuleFor(x => x.CaloriesBurned!.Value).GreaterThanOrEqualTo(0));
    }
}
