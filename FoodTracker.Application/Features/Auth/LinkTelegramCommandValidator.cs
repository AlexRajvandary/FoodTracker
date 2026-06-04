using FluentValidation;

namespace FoodTracker.Application.Features.Auth;

public class LinkTelegramCommandValidator : AbstractValidator<LinkTelegramCommand>
{
    public LinkTelegramCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.InitData).NotEmpty();
    }
}
