namespace FoodTracker.Web.Options;

/// <summary>Секция конфигурации <c>Telegram</c> (тот же BotToken, что для бота).</summary>
public sealed class TelegramAuthOptions
{
    public const string SectionName = "Telegram";

    public string BotToken { get; set; } = string.Empty;
}
