using Microsoft.Extensions.DependencyInjection;

namespace FoodTracker.Telegram.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Telegram module registration (scaffolding only).
    /// </summary>
    public static IServiceCollection AddFoodTrackerTelegram(this IServiceCollection services)
    {
        // TODO: Register Telegram.Bot clients, update handlers, polling/webhook wiring, etc.
        return services;
    }
}

