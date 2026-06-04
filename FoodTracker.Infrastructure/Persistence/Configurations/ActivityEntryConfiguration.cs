using FoodTracker.Domain.Activities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodTracker.Infrastructure.Persistence.Configurations;

public class ActivityEntryConfiguration : IEntityTypeConfiguration<ActivityEntry>
{
    public void Configure(EntityTypeBuilder<ActivityEntry> entity)
    {
        entity.ToTable("activity_entries");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.ActivityName).HasMaxLength(500).IsRequired();
        entity.Property(e => e.CaloriesBurned).HasPrecision(18, 4);
        entity.HasIndex(e => new { e.UserId, e.OccurredAtUtc });
        entity
            .HasOne<ActivityType>()
            .WithMany()
            .HasForeignKey(e => e.ActivityTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
