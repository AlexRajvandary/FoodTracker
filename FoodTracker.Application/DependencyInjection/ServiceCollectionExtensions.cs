using FluentValidation;
using FoodTracker.Application.Behaviors;
using FoodTracker.Application.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FoodTracker.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFoodTrackerApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AssemblyReference>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}