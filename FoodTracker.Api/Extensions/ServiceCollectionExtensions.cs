using System.Text;
using FoodTracker.Application.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FoodTracker.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFoodTrackerWebPresentation(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        var jwt = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>() ?? new JwtOptions();
        if (string.IsNullOrWhiteSpace(jwt.SigningKey) || jwt.SigningKey.Length < 32)
        {
            throw new InvalidOperationException(
                "В конфигурации задайте Jwt:SigningKey длиной не менее 32 символов (см. appsettings.Development.json).");
        }

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SigningKey)),
                    ClockSkew = TimeSpan.FromMinutes(1),
                };
            });

        services.AddAuthorization();

        services.AddCors(o => o.AddDefaultPolicy(p => p
            .WithOrigins(
                "http://localhost:5173",
                "https://localhost:5173",
                "https://web.telegram.org")
            .AllowAnyHeader()
            .AllowAnyMethod()));

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddHealthChecks();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "FoodTracker API",
                    Version = "v1",
                });
            options.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Description = "JWT в заголовке: Authorization: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                });
            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        Array.Empty<string>()
                    },
                });
        });

        return services;
    }
}
