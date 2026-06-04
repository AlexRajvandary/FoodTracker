namespace FoodTracker.Domain.Auth;

/// <summary>Способ входа / привязки аккаунта. В БД хранится как целое (int).</summary>
public enum AuthProviderKind : int
{
    EmailPassword = 0,
    Telegram = 1,
    PhoneOtp = 2,
    Google = 3,
}
