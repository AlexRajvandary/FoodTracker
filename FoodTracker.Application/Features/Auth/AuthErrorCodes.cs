namespace FoodTracker.Application.Features.Auth;

public static class AuthErrorCodes
{
    public const string Unauthorized = "auth.unauthorized";
    public const string Conflict = "auth.conflict";
    public const string Identity = "auth.identity";
    public const string TelegramNotConfigured = "telegram.not_configured";
    public const string TelegramInvalidInitData = "telegram.invalid_init_data";
    public const string TelegramMissingUser = "telegram.missing_user";
    public const string NotFound = "auth.not_found";
}
