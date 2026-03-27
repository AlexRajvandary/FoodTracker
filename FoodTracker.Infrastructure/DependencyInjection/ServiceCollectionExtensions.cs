using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodTracker.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Infrastructure module registration (scaffolding only).
    /// </summary>
    public static IServiceCollection AddFoodTrackerInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: Register EF Core DbContext, repositories, and other infrastructure services.
        // Connection string placeholder:
        _ = configuration.GetConnectionString("PostgreSql");

        return services;
    }
}

