using FoodTracker.Domain.Auth;
using FoodTracker.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodTracker.Infrastructure.Persistence.Configurations;

public class UserAuthProviderConfiguration : IEntityTypeConfiguration<UserAuthProvider>
{
    public void Configure(EntityTypeBuilder<UserAuthProvider> entity)
    {
        entity.ToTable("user_auth_providers");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.ProviderKind).HasConversion<int>();
        entity.Property(e => e.ProviderKey).HasMaxLength(450).IsRequired();
        entity.HasIndex(e => new { e.ProviderKind, e.ProviderKey }).IsUnique();
        entity
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
