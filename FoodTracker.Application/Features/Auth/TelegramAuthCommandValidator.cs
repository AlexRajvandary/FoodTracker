using FluentValidation;

namespace FoodTracker.Application.Features.Auth;

public class TelegramAuthCommandValidator : AbstractValidator<TelegramAuthCommand>
{
    public TelegramAuthCommandValidator()
    {
        RuleFor(x => x.InitData).NotEmpty();
    }
}
