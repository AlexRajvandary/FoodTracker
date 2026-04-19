using FoodTracker.Application.DependencyInjection;
using FoodTracker.Infrastructure.DependencyInjection;
using FoodTracker.Infrastructure.Persistence;
using FoodTracker.Telegram.DependencyInjection;
using FoodTracker.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFoodTrackerInfrastructure(builder.Configuration);
builder.Services.AddFoodTrackerApplication();
builder.Services.AddFoodTrackerWebPresentation(builder.Configuration);
builder.Services.AddFoodTrackerTelegram();

var app = builder.Build();

app.UseGlobalExceptionHandling();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    await db.Database.MigrateAsync().ConfigureAwait(false);

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    foreach (var roleName in new[] { "user", "admin" })
    {
        if (!await roleManager.RoleExistsAsync(roleName).ConfigureAwait(false))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(roleName)).ConfigureAwait(false);
        }
    }
}

app.MapControllers();
await app.RunAsync().ConfigureAwait(false);
