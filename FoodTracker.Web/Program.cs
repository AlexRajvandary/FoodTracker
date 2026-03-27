using FoodTracker.Application;
using FoodTracker.Infrastructure.DependencyInjection;
using FoodTracker.Telegram.DependencyInjection;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// REST API host scaffolding (no controllers yet)
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MediatR (no handlers yet)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(typeof(AssemblyReference).Assembly));

// Infrastructure & Telegram modules (scaffolding only)
builder.Services.AddFoodTrackerInfrastructure(builder.Configuration);
builder.Services.AddFoodTrackerTelegram();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
