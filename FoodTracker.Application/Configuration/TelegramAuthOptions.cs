namespace FoodTracker.Application.Configuration;

public class TelegramAuthOptions
{
    public const string SectionName = "Telegram";

    public string BotToken { get; set; } = string.Empty;
}
