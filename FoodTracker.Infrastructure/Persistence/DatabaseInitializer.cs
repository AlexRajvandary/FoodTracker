using FoodTracker.Domain.Auth;
using FoodTracker.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoodTracker.Infrastructure.Persistence;

/// <summary>Идемпотентное наполнение локальной БД (только Development).</summary>
public static class DatabaseInitializer
{
    private const string TestEmail = "test@example.com";
    private const string TestPassword = "testtest";

    public static async Task EnsureTestUserAsync(
        IServiceProvider scopedServices,
        IHostEnvironment environment,
        CancellationToken cancellationToken = default)
    {
        if (!environment.IsDevelopment())
        {
            return;
        }

        var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();
        var db = scopedServices.GetRequiredService<DataContext>();

        var existing = await userManager.FindByEmailAsync(TestEmail).ConfigureAwait(false);
        if (existing is not null)
        {
            await EnsureRoleAndProviderAsync(existing, userManager, db, cancellationToken).ConfigureAwait(false);
            return;
        }

        var user = new ApplicationUser
        {
            UserName = TestEmail,
            Email = TestEmail,
            FirstName = "test",
            EmailConfirmed = true,
        };

        var create = await userManager.CreateAsync(user, TestPassword).ConfigureAwait(false);
        if (!create.Succeeded)
        {
            throw new InvalidOperationException(
                "Не удалось создать тестового пользователя: "
                + string.Join("; ", create.Errors.Select(static e => e.Description)));
        }

        try
        {
            await EnsureRoleAndProviderAsync(user, userManager, db, cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            await userManager.DeleteAsync(user).ConfigureAwait(false);
            throw;
        }
    }

    private static async Task EnsureRoleAndProviderAsync(
        ApplicationUser user,
        UserManager<ApplicationUser> userManager,
        DataContext db,
        CancellationToken cancellationToken)
    {
        if (!await userManager.IsInRoleAsync(user, "user").ConfigureAwait(false))
        {
            var roleAdd = await userManager.AddToRoleAsync(user, "user").ConfigureAwait(false);
            if (!roleAdd.Succeeded)
            {
                throw new InvalidOperationException(
                    "Не удалось назначить роль тестовому пользователю: "
                    + string.Join("; ", roleAdd.Errors.Select(static e => e.Description)));
            }
        }

        var normalizedEmail = userManager.NormalizeEmail(TestEmail);
        if (string.IsNullOrEmpty(normalizedEmail))
        {
            throw new InvalidOperationException("NormalizeEmail вернул пустое значение для тестового пользователя.");
        }

        var providerExists = await db.UserAuthProviders.AnyAsync(
                x => x.ProviderKind == AuthProviderKind.EmailPassword && x.ProviderKey == normalizedEmail,
                cancellationToken)
            .ConfigureAwait(false);
        if (!providerExists)
        {
            db.UserAuthProviders.Add(
                new UserAuthProvider
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    ProviderKind = AuthProviderKind.EmailPassword,
                    ProviderKey = normalizedEmail,
                    CreatedAtUtc = DateTime.UtcNow,
                });
            await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
