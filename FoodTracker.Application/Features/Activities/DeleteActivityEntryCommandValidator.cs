using FluentValidation;

namespace FoodTracker.Application.Features.Activities;

public sealed class DeleteActivityEntryCommandValidator : AbstractValidator<DeleteActivityEntryCommand>
{
    public DeleteActivityEntryCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.EntryId).NotEmpty();
    }
}
