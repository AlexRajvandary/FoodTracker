using FoodTracker.Application.DependencyInjection;
using FoodTracker.Infrastructure.DependencyInjection;
using FoodTracker.Telegram.DependencyInjection;
using FoodTracker.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFoodTrackerWebPresentation();
builder.Services.AddFoodTrackerApplication();
builder.Services.AddFoodTrackerInfrastructure(builder.Configuration);
builder.Services.AddFoodTrackerTelegram();

var app = builder.Build();

app.UseGlobalExceptionHandling();
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
