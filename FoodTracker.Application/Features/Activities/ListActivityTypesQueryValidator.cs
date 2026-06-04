using FluentValidation;

namespace FoodTracker.Application.Features.Activities;

public sealed class ListActivityTypesQueryValidator : AbstractValidator<ListActivityTypesQuery>
{
    public ListActivityTypesQueryValidator()
    {
        When(x => !string.IsNullOrEmpty(x.Query), () => RuleFor(x => x.Query!).MinimumLength(1));
    }
}
