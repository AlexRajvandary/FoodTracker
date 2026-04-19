using FluentValidation;

namespace FoodTracker.Application.Features.Activities;

public sealed class ListActivityTypesQueryValidator : AbstractValidator<ListActivityTypesQuery>
{
    public ListActivityTypesQueryValidator()
    {
        When(x => !string.IsNullOrEmpty(x.Q), () => RuleFor(x => x.Q!).MinimumLength(1));
    }
}
