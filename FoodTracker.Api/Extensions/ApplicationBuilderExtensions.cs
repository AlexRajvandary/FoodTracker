using FoodTracker.Api.Middleware;
using Microsoft.AspNetCore.Builder;

namespace FoodTracker.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
