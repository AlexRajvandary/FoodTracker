using FoodTracker.Application.Abstractions.Services;
using FoodTracker.Application.Configuration;
using FoodTracker.Infrastructure.Identity;
using FoodTracker.Infrastructure.Persistence;
using FoodTracker.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodTracker.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFoodTrackerInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        var connectionString = configuration.GetConnectionString("PostgreSql");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'PostgreSql' is not configured.");
        }

        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.Configure<TelegramAuthOptions>(configuration.GetSection(TelegramAuthOptions.SectionName));
        services.AddHttpContextAccessor();
        services.AddScoped<IAuthAccountService, AuthAccountService>();

        services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));

        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<DataContext>();

        return services;
    }
}