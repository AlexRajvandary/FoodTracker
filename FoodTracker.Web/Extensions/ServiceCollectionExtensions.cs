using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FoodTracker.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFoodTrackerWebPresentation(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddHealthChecks();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FoodTracker API",
                Version = "v1"
            });
        });
        return services;
    }
}
